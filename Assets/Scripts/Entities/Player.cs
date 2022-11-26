using Movers;
using UnityEngine;
using VFX;
using static UnityEngine.InputSystem.InputAction;

namespace Entities
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private AbstractMover _mover;
        [SerializeField] private SimpleVFX _jumpDust;
        [SerializeField] private GameObject _dustSpawn;
        [SerializeField] private float _flipSpeed = 0.1f;

        private Input _input;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private float _horizontalMoveRatio;

        private void Awake()
        {
            _input = new Input();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mover ??= gameObject.AddComponent<NonMover>();
        }

        private void OnEnable()
        {
            EnableInput();
            _mover.MovedY += ThrowDust;
        }

        private void OnDisable()
        {
            DisableInput();
            _mover.MovedY -= ThrowDust;
        }

        private void FixedUpdate()
        {
            _mover.MoveHorizontally(_horizontalMoveRatio);
            UpdateView();
        }

        private void EnableInput()
        {
            _input.Enable();
            _input.Gameplay.Move.performed += SetMoveRatio;
            _input.Gameplay.Move.canceled += ResetMoveRatio;
            _input.Gameplay.Jump.performed += Jump;
        }

        private void DisableInput()
        {
            _input.Gameplay.Move.performed -= SetMoveRatio;
            _input.Gameplay.Move.canceled -= ResetMoveRatio;
            _input.Gameplay.Jump.performed -= Jump;
            _input.Disable();
        }

        private void SetMoveRatio(CallbackContext data) => _horizontalMoveRatio = data.ReadValue<float>();
        
        private void ResetMoveRatio(CallbackContext data) => _horizontalMoveRatio = default;

        private void Jump(CallbackContext data)
        {
            var ratio = Mathf.Sign(data.ReadValue<float>());
            _mover.MoveVertically(ratio);
        }

        private void ThrowDust()
        {
            Instantiate(_jumpDust, _dustSpawn.transform.position, Quaternion.identity);
        }

        private void UpdateView()
        {
            var speed = _mover.GetSpeed();
            
            SetAnimatorSpeeds(speed);
            FlipSprite(speed);
        }

        private void FlipSprite(Vector2 speed)
        {
            if (speed.x < -_flipSpeed)
                _spriteRenderer.flipX = true;
            else if (speed.x > _flipSpeed)
                _spriteRenderer.flipX = false;
        }

        private void SetAnimatorSpeeds(Vector2 speed)
        {
            _animator.SetFloat(AnimatorHashes.SpeedX, Mathf.Abs(speed.x));
            _animator.SetFloat(AnimatorHashes.SpeedY, speed.y);
        }

        private static class AnimatorHashes
        {
            public static readonly int SpeedX = Animator.StringToHash("AbsSpeedX");
            public static readonly int SpeedY = Animator.StringToHash("SpeedY");
        }
    }
}