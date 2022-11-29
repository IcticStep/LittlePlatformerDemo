using System;
using Entities.Functions;
using EntitiesFunctions;
using EntitiesFunctions.Movers;
using UnityEngine;

namespace Entities.Controls.AI
{
    [RequireComponent(typeof(CollisionDetector))]
    public class OneSideUntilCollisionController : MonoBehaviour
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private Side _direction;
        
        private CollisionDetector _collisionDetector;
        
        private void Awake() => _collisionDetector = GetComponent<CollisionDetector>();
        private void OnEnable() => _collisionDetector.OnCollision += StopMoving;
        private void OnDisable() => _collisionDetector.OnCollision += StopMoving;
        private void Start() => StartMoving();
        private void StartMoving() => _mover.MoveHorizontally((float)_direction);
        private void StopMoving() => _mover.MoveHorizontally(0f);

        private enum Side : int
        {
            Left = -1,
            None = 0,
            Right = 1
        }
    }
}