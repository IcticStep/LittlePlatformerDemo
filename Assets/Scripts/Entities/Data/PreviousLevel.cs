namespace Entities.Data
{
    public struct PreviousLevel
    {
        public string Name { get; }
        public Edge? Crossed { get; }

        public PreviousLevel(string name, Edge? crossed)
        {
            Name = name;
            Crossed = crossed;
        }
    }
}