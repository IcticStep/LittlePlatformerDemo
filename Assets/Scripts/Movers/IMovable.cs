namespace Movers
{
    public interface IMovable
    {
        public void MoveHorizontally(float ratio);
        public void MoveVertically(float ratio);
        public float GetSpeed();
    }
}
