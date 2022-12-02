using System;
using UnityEngine;

namespace Entities.Functions.Movers
{
    public abstract class Mover : MonoBehaviour
    {
        public event Action OnMoveX;
        public event Action OnMoveY;
        
        public abstract void MoveHorizontally(float ratio);
        public abstract void MoveVertically(float ratio);
        public abstract Vector2 GetSpeed();

        public void Move(Vector2 ratios)
        {
            MoveHorizontally(ratios.x);
            MoveVertically(ratios.y);
        }

        protected void SignalMovingX() => OnMoveX?.Invoke();
        protected void SignalMovingY() => OnMoveY?.Invoke();
    }
}