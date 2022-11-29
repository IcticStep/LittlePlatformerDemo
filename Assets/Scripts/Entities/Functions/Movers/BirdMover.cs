using UnityEngine;

namespace EntitiesFunctions.Movers
{
    public class BirdMover : Mover
    {
        [SerializeField] private float _speed = 2f;

        private Vector2 _ratios = Vector2.zero;
        private Vector2 CurrentMove => _speed * Time.fixedDeltaTime * _ratios;

        private void FixedUpdate() => transform.Translate(CurrentMove);
        public override void MoveHorizontally(float ratio) => _ratios.x = ratio;
        public override void MoveVertically(float ratio) => _ratios.y = ratio;
        public override Vector2 GetSpeed() => CurrentMove;
    }
}