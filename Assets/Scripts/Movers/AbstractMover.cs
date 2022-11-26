using UnityEngine;

namespace Movers
{
    public abstract class AbstractMover : MonoBehaviour
    {
        protected static float GravityStrength = 9.8f;
        
        public abstract void MoveHorizontally(float ratio);
        public abstract void MoveVertically(float ratio);
        public abstract float GetSpeed();

        public void Move(Vector2 ratios)
        {
            MoveHorizontally(ratios.x);
            MoveVertically(ratios.y);
        }
    }
}