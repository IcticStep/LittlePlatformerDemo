using Entities.Functions;
using Entities.Functions.Movers;
using Entities.System;
using UnityEngine;
using Zenject;
using static UnityEngine.InputSystem.InputAction;

namespace Entities.Controls
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(DeathMaker))]
    public class Player : MonoBehaviour
    {
        private ILevelSwitcher _levelSwitcher;
        private Mover _mover;
        private DeathMaker _deathMaker;
        private Input _input;

        private float _horizontalMoveRatio;

        [Inject]
        public void Construct(ILevelSwitcher levelSwitcher) => _levelSwitcher = levelSwitcher;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _deathMaker = GetComponent<DeathMaker>();
            _input = new Input();
        }

        private void OnEnable()
        {
            EnableInput();
            _deathMaker.OnDie += DisableInput;
            _levelSwitcher.OnLevelStart += EnableInput;
        }

        private void OnDisable()
        {
            DisableInput();
            _deathMaker.OnDie -= DisableInput;
            _levelSwitcher.OnLevelStart -= EnableInput;
        }

        private void FixedUpdate() => _mover.MoveHorizontally(_horizontalMoveRatio);

        public void EnableInput()
        {
            _input.Enable();
            _input.Gameplay.Move.performed += SetMoveRatio;
            _input.Gameplay.Move.canceled += ResetMoveRatio;
            _input.Gameplay.Jump.performed += Jump;
        }

        public void DisableInput()
        {
            _horizontalMoveRatio = default;
            _input.Gameplay.Move.performed -= SetMoveRatio;
            _input.Gameplay.Move.canceled -= ResetMoveRatio;
            _input.Gameplay.Jump.performed -= Jump;
            _input.Disable();
        }

        private void SetMoveRatio(CallbackContext input) => _horizontalMoveRatio = input.ReadValue<float>();
        private void ResetMoveRatio(CallbackContext input) => _horizontalMoveRatio = default;

        private void Jump(CallbackContext input)
        {
            var ratio = Mathf.Sign(input.ReadValue<float>());
            _mover.MoveVertically(ratio);
        }
    }
}