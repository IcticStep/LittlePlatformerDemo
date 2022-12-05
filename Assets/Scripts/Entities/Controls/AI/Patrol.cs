using System;
using Entities.Functions;
using Entities.Functions.Movers;
using UnityEngine;

namespace Entities.Controls.AI
{
    [RequireComponent(typeof(Mover))]
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private Side _currentDirection;
        
        private Mover _mover;

        private void Awake() => _mover = GetComponent<Mover>();
        private void OnEnable() => _collisionDetector.OnCollision += ChangeMovingDirection;
        private void OnDisable() => _collisionDetector.OnCollision -= ChangeMovingDirection;
        private void FixedUpdate() => Move();

        private void ChangeMovingDirection(Collider2D otherCollider) => _currentDirection = (Side)((int)_currentDirection * -1);
        private void Move() => _mover.MoveHorizontally((float)_currentDirection);

        private enum Side
        {
            Left = -1,
            None = 0,
            Right = 1
        }
    }
}