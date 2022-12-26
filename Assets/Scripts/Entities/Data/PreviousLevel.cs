namespace Entities.Data
{
    public struct PreviousLevel
    {
        public int Id { get; }
        public Edge? Crossed { get; }

        public PreviousLevel(int id, Edge? crossed)
        {
            Id = id;
            Crossed = crossed;
        }
    }
}