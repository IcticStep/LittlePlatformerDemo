using System;
using System.Collections.Generic;
using Entities.System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Configurations
{
    [CreateAssetMenu(fileName = "LevelSwitch", menuName = "ScriptableObjects/LevelSwitchConfiguration", order = 1)]
    public class LevelSwitchConfiguration : ScriptableObject
    {
        private const string _levelPrefix = "Level";
        private const string _levelPath = "Assets/Scenes/Levels/";
        private const string _levelFileType = ".unity";
        
        public List<EdgeSettings> EdgeSettings;
        private string _lastFullPathChecked = "";

        private void OnValidate()
        {
            for (var i = 0; i < EdgeSettings.Count; i++)
                ValidateEdgeSetting(i);
        }

        private void ValidateEdgeSetting(int index)
        {
            var edgeSettings = EdgeSettings[index];
            if(edgeSettings.Action != EdgeAction.SwitchLevel)
                return;
            
            if (SceneExists($"{_levelPrefix}{edgeSettings.GoalSceneIndex}"))
                return;
            
            edgeSettings.GoalSceneIndex = 0;
            EdgeSettings[index] = edgeSettings;
            throw new Exception($"Invalid scene index entered: there is no scene with path {_lastFullPathChecked}.");
        }

        private bool SceneExists(string shortName)
        {
            _lastFullPathChecked = _levelPath + shortName + _levelFileType;
            var buildIndex = SceneUtility.GetBuildIndexByScenePath(_lastFullPathChecked);

            return buildIndex >= 0 && buildIndex <= SceneManager.sceneCountInBuildSettings;
        }
    }
}