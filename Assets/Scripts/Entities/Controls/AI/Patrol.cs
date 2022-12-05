using Entities.Functions;
using Entities.Functions.Movers;
using UnityEngine;

namespace Entities.Controls.AI
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private Side _currentDirection;

        private void OnEnable() => _collisionDetector.OnCollision += HandleCollision;
        private void OnDisable() => _collisionDetector.OnCollision -= HandleCollision;
        private void FixedUpdate() => Move();

        private void HandleCollision(Collider2D otherCollider) => ChangeMovingDirection();
        private void Move() => _mover.MoveHorizontally((float)_currentDirection);
        private void ChangeMovingDirection() => _currentDirection = (Side)((int)_currentDirection * -1);

        private enum Side
        {
            Left = -1,
            None = 0,
            Right = 1
        }
    }
}