using Entities.Functions.Movers;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Entities.Controls
{
    public class PlayerInput : MonoBehaviour
    {
        private Mover _mover;
        private Input _input;
        private float _horizontalMoveRatio;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _input = new Input();
        }

        private void OnEnable() => EnableInput();
        private void OnDisable() => DisableInput();
        private void FixedUpdate() => _mover.MoveHorizontally(_horizontalMoveRatio);

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
    }
}