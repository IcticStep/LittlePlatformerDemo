using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Entities.Functions
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class DeathMaker : MonoBehaviour
    {
        [SerializeField] private List<CollisionDetector> _weakPoints;
        [SerializeField] private float _destroyingSecondsDelay = 10f;
        [SerializeField] private bool _applyKillingTorque;
        [SerializeField] private float _onKilledTorque = 2f;
        [SerializeField] private bool _makeJump;
        [SerializeField] private float _jumpForce = 3f;

        public event Action OnDie;
        public bool Died { get; private set; }

        private const float _dieFallGravityScale = 1f;
        
        private Rigidbody2D _rigidbody;
        private List<Collider2D> _attachedColliders;
        private Sequence _selfDestroying;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _attachedColliders = GetComponentsInChildren<Collider2D>().ToList();
        }

        private void OnEnable() => _weakPoints.ForEach(weekPoint => weekPoint.OnCollision += Die);
        private void OnDisable() => _weakPoints.ForEach(weekPoint => weekPoint.OnCollision -= Die);
        private void OnDestroy() => _selfDestroying.Kill();

        private void Die(Collider2D otherCollider)
        {
            if(Died || OtherIsDead(otherCollider))
                return;

            MarkSelfDied();

            StartPhysicFall();
            AdjustCollisionJump();
            AdjustCollisionTorque(otherCollider);

            DestroySelfDelayed();
        }

        private void MarkSelfDied()
        {
            Died = true;
            OnDie?.Invoke();
        }

        private bool OtherIsDead(Collider2D otherCollider)
        {
            var otherDeathMaker = otherCollider.GetComponentInParent<DeathMaker>();
            return otherDeathMaker != null && otherDeathMaker.Died;
        }

        private void AdjustCollisionJump()
        {
            if(!_makeJump)
                return;

            _rigidbody.AddForce(new Vector2(0,_jumpForce), ForceMode2D.Impulse);
        }

        private void AdjustCollisionTorque(Component other)
        {
            if(!_applyKillingTorque)
                return;
            
            var horizontalDirection = Mathf.Sign(transform.position.x - other.transform.position.x);
            _rigidbody.AddTorque(_onKilledTorque * horizontalDirection, ForceMode2D.Impulse);
        }

        private void StartPhysicFall()
        {
            StopCollisions();
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.gravityScale = _dieFallGravityScale;
        }

        // ReSharper disable once ParameterHidesMember
        private void StopCollisions() => _attachedColliders.ForEach(collider => collider.enabled = false);
        
        private void DestroySelfDelayed() => 
            _selfDestroying ??= DOTween.Sequence()
                .AppendInterval(_destroyingSecondsDelay)
                .AppendCallback(() =>
                {
                    if (gameObject != null) 
                        Destroy(gameObject);
                });
    }
}