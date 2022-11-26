using System;
using UnityEngine;

namespace Movers
{
    public abstract class AbstractMover : MonoBehaviour
    {
        public event Action MovedX;
        public event Action MovedY;
        
        public abstract void MoveHorizontally(float ratio);
        public abstract void MoveVertically(float ratio);
        public abstract Vector2 GetSpeed();

        public void Move(Vector2 ratios)
        {
            MoveHorizontally(ratios.x);
            MoveVertically(ratios.y);
        }

        protected void SignalMovingX() => MovedX?.Invoke();
        protected void SignalMovingY() => MovedY?.Invoke();
    }
}