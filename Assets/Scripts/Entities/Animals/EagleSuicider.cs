using DG.Tweening;
using Movers;
using UnityEngine;

namespace Entities.Animals
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AbstractMover))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class EagleSuicider : MonoBehaviour
    {
        [SerializeField] private float _startDirection = -1f;
        [SerializeField] private float _destroyingDelay = 3f;

        private static readonly int _fallingAnimatorVar = Animator.StringToHash("Hurting");
        
        private Rigidbody2D _rigidbody;
        private AbstractMover _mover;
        private Collider2D _collider;
        private Animator _animator;
        private float _startGravityScale;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _mover = GetComponent<AbstractMover>();
            _collider = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();

            _startGravityScale = _rigidbody.gravityScale;
        }

        private void Start()
        {
            _rigidbody.gravityScale = 0;
            _mover.MoveHorizontally(_startDirection);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            StopFlying();
            StartFalling();
            DestroySelfDelayed();
        }

        private void StartFalling()
        {
            _collider.enabled = false;
            _mover.enabled = false;
            _rigidbody.gravityScale = _startGravityScale;
            _animator.SetBool(_fallingAnimatorVar, true);
        }

        private void StopFlying() => _mover.MoveHorizontally(0f);

        private void DestroySelfDelayed() => DOTween.Sequence()
                .AppendInterval(_destroyingDelay)
                .AppendCallback(() => Destroy(gameObject));
    }
}