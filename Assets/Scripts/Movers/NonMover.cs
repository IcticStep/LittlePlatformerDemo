using UnityEngine;

namespace Movers
{
    public class NonMover : AbstractMover
    {
        public override void MoveHorizontally(float ratio) { }
        public override void MoveVertically(float ratio) { }
        public override Vector2 GetSpeed() => Vector2.zero;
    }
}