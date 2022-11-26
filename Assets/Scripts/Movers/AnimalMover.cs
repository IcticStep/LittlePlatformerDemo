using System;
using UnityEngine;

namespace Movers
{
    [RequireComponent(typeof(CharacterController))]
    public class AnimalMover : AbstractMover
    {
        [SerializeField] private float _speed = 5;
        
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        public override void MoveHorizontally(float ratio)
        {
            _controller.Move(new Vector2(_speed * ratio * Time.fixedDeltaTime, 0));
        }

        public override void MoveVertically(float ratio)
        {
            throw new NotImplementedException();
        }

        public override float GetSpeed()
        {
            throw new NotImplementedException();
        }
    }
}