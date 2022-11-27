using System;
using UnityEngine;

namespace Movers
{
    public class BirdMover : AbstractMover
    {
        [SerializeField] private float _speed = 2f;

        private Vector2 _ratios = Vector2.zero;
        private Vector2 GetCurrentMove => _speed * Time.fixedDeltaTime * _ratios;

        private void FixedUpdate()
        {
            transform.Translate(GetCurrentMove);
        }

        public override void MoveHorizontally(float ratio) => _ratios.x = ratio;
        public override void MoveVertically(float ratio) => _ratios.y = ratio;
        public override Vector2 GetSpeed() => GetCurrentMove;
    }
}