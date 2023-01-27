using DependenciesManagement;
using Entities.Data;
using Entities.System;
using UnityEngine;
using Zenject;

namespace Entities.Functions.DoorsSystem
{
    public class Door : MonoBehaviour, IDoor
    {
        private const int DefaultLevelValue = 1;
        public int GoalLevel = DefaultLevelValue;

        private ILevelSwitcher _levelSwitcher;
        private DoorContainer _doorContainer;
#if UNITY_EDITOR
        private readonly LevelIDValidator _levelIDValidator = new();        
#endif
        
        [Inject]
        public void Construct(ILevelSwitcher levelSwitcher, DoorContainer doorContainer)
        {
            _levelSwitcher = levelSwitcher;
            _doorContainer = doorContainer;
        }

        private void Awake() => _doorContainer.AddDoor(this);
        private void OnValidate() => ValidateGoalLevel();

        public void Open()
        {
            _levelSwitcher.SwitchLevel(GoalLevel);
        }

        public int GetGoalLevel() => GoalLevel;


        private void ValidateGoalLevel()
        {
            if (_levelIDValidator.LevelExists(GoalLevel))
                return;
            
            Debug.LogError($"There is no level indexed \"{GoalLevel}\". [{this}]");
            GoalLevel = DefaultLevelValue;
        }

        public override string ToString() => $"Door to level {GoalLevel}. Coordinates: {transform.position}.";
    }
}
