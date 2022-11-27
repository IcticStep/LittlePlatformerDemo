using UnityEngine;

namespace Movers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AnimalMover : AbstractMover
    {
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Transform _groundCheckerEndPoint;
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _jumpHeight = 100;
        [SerializeField] private float _groundCastRadius = 0.5f;
        
        private Rigidbody2D _rigidbody;
        
        private float _airJumpHeight;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _airJumpHeight = Mathf.Abs(_groundCheckerEndPoint.position.y - transform.position.y);
        }

        public override void MoveHorizontally(float ratio)
        {
            _rigidbody.velocity = new Vector2(_speed * ratio, _rigidbody.velocity.y);

            if (Mathf.Abs(ratio) <= float.Epsilon)
                return;
            SignalMovingX();
        }

        public override void MoveVertically(float ratio)
        {
            if (ratio < 0)
            {
                Debug.LogError("Can't jump down!", this);
                return;
            }
            
            if(!IsOnGround())
                return;

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpHeight);
            SignalMovingY();
        }

        private bool IsOnGround()
        {
            var hit = Physics2D.CircleCast(transform.position, _groundCastRadius, Vector2.down, _airJumpHeight, _groundMask);
            return hit.collider is not null;
        }

        public override Vector2 GetSpeed() => _rigidbody.velocity;
    }
}