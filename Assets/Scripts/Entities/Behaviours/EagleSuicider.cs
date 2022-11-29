using DG.Tweening;
using Entities.Functions;
using EntitiesFunctions;
using UnityEngine;

namespace Entities.Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(CollisionDetector))]
    public class EagleSuicider : Enemy
    {
        [SerializeField] private float _destroyingDelay = 3f;

        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private CollisionDetector _collisionDetector;
        private Sequence _selfDestroying;
        
        private float _startGravityScale;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _collisionDetector = GetComponent<CollisionDetector>();

            _startGravityScale = _rigidbody.gravityScale;
        }

        private void Start() => TurnOffGravity();
        private void OnEnable() => _collisionDetector.OnCollision += Die;
        private void OnDisable() => _collisionDetector.OnCollision -= Die;
        private void OnDestroy() => _selfDestroying.Kill();

        private void Die()
        {
            SignalDying();
            StartPhysicFall();
            DestroySelfDelayed();
        }

        private void TurnOffGravity() => _rigidbody.gravityScale = 0;

        private void StartPhysicFall()
        {
            _collider.enabled = false;
            _rigidbody.gravityScale = _startGravityScale;
        }

        private void DestroySelfDelayed() => _selfDestroying = DOTween.Sequence()
                .AppendInterval(_destroyingDelay)
                .AppendCallback(() => Destroy(gameObject));
    }
}