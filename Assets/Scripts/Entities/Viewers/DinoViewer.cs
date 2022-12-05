using System;
using Entities.Functions.Movers;
using UnityEngine;

namespace Entities.Viewers
{
    [RequireComponent(typeof(Mover))]
    public class DinoViewer : Viewer
    {
        private static readonly int _speed = Animator.StringToHash("Speed");
        
        private Mover _mover;

        private void FixedUpdate()
        {
            var speed = _mover.GetSpeed();
            SetAnimatorSpeed(speed);
        }

        protected override void DoAdditionalInitialization() => _mover = GetComponent<Mover>();
        
        private void SetAnimatorSpeed(Vector2 speed) => Animator.SetFloat(_speed, Mathf.Abs(speed.x));
    }
}