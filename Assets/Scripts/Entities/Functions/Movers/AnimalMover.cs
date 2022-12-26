using System;
using UnityEngine;

namespace Entities.Functions.Movers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AnimalMover : Mover
    {
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Transform _groundCheckerEndPoint;
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _jumpVelocity = 7;
        [SerializeField] private float _groundCastRadius = 0.5f;
        [SerializeField] private float _velocityPercentageJumpReturn = 0.3f;
        
        private Rigidbody2D _rigidbody;
        private float _distanceGroundContact;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            if(_groundCheckerEndPoint != null)
                _distanceGroundContact = Mathf.Abs(_groundCheckerEndPoint.position.y - transform.position.y);
        }

        private void FixedUpdate()
        {
            CorrectJump();
        }

        public override void MoveHorizontally(float ratio)
        {
            _rigidbody.velocity = new Vector2(_speed * ratio, _rigidbody.velocity.y);

            var movementIsSmall = Mathf.Abs(ratio) <= float.Epsilon;
            if (movementIsSmall)
                return;
            
            SignalMovingX();
        }

        public override void MoveVertically(float ratio)
        {
            if (ratio <= 0)
            {
                Debug.LogError("Can't jump down!", this);
                return;
            }
            
            if(!IsOnGround())
                return;

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpVelocity);
            SignalMovingY();
        }

        public override Vector2 GetSpeed() => _rigidbody.velocity;

        private void CorrectJump()
        {
            var y = Math.Abs(_rigidbody.velocity.y);
            var needCorrection = y > 1 && y < (_jumpVelocity * _velocityPercentageJumpReturn);
            if(!needCorrection || IsOnGround())
                return;

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -y);
        }

        private bool IsOnGround()
        {
            var hit = Physics2D.CircleCast(transform.position,
                _groundCastRadius, Vector2.down, _distanceGroundContact, _groundMask);
            return hit.collider is not null;
        }
    }
}