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
        public List<EdgeSettings> EdgeSettings;
        
        private const string LevelPrefix = "Level";
        private const string LevelPath = "Assets/Scenes/Levels/";
        private const string LevelFileType = ".unity";

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
            
            if (SceneExists($"{LevelPrefix}{edgeSettings.GoalSceneIndex}"))
                return;
            
            edgeSettings.GoalSceneIndex = 0;
            EdgeSettings[index] = edgeSettings;
            throw new Exception($"Invalid scene index entered: there is no scene with path {_lastFullPathChecked}.");
        }

        private bool SceneExists(string shortName)
        {
            _lastFullPathChecked = LevelPath + shortName + LevelFileType;
            var buildIndex = SceneUtility.GetBuildIndexByScenePath(_lastFullPathChecked);

            return buildIndex >= 0 && buildIndex <= SceneManager.sceneCountInBuildSettings;
        }
    }
}