using System.Collections.Generic;
using System.Linq;
using Entities.Functions.DoorsSystem;

namespace DependenciesManagement
{
    public class DoorContainer
    {
        private readonly List<Door> _doors = new ();

        public void AddDoor(Door door) => _doors.Add(door);
        public Door GetDoorToLevel(int levelIndex) => _doors.FirstOrDefault(door => door.GoalLevel == levelIndex);
        public bool HasDoorToLevel(int levelIndex) => _doors.Any(door => door.GoalLevel == levelIndex);
    }
}