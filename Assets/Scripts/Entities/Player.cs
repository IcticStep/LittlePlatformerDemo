using System;
using Movers;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        [SerializeReference] private AbstractMover _mover;

        private Input _input;
        private float _horizontalMoveRatio;

        private void Awake()
        {
            _input = new Input();
        }

        private void OnEnable()
        {
            EnableInput();
        }

        private void OnDisable()
        {
            DisableInput();
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
            _input.Disable();
            _input.Gameplay.Move.performed -= SetMoveRatio;
            _input.Gameplay.Move.canceled -= ResetMoveRatio;
            _input.Gameplay.Jump.performed -= Jump;
        }

        private void FixedUpdate()
        {
            _mover.MoveHorizontally(_horizontalMoveRatio);
        }

        private void SetMoveRatio(CallbackContext callbackContext) => _horizontalMoveRatio = callbackContext.ReadValue<float>();
        private void ResetMoveRatio(CallbackContext obj) => _horizontalMoveRatio = default;

        private void Jump(CallbackContext obj)
        {
            var ratio = Mathf.Sign(obj.ReadValue<float>());
            _mover.MoveVertically(ratio);
        }
    }
}
