using UnityEngine;

namespace EntitiesFunctions.Movers
{
    public class NonMover : Mover
    {
        public override void MoveHorizontally(float ratio) { }
        public override void MoveVertically(float ratio) { }
        public override Vector2 GetSpeed() => Vector2.zero;
    }
}