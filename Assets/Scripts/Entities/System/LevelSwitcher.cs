using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Data;
using Entities.System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities.System
{
    public class LevelSwitcher : MonoBehaviour, ILevelSwitcher
    {
        private readonly LevelIndexer _levelIndexer = new();
        
        public int PreviousLevel { get; private set; }
        public int GetPreviousLevel() => PreviousLevel;

        public event Action OnLevelStart;
        public event Action OnLevelSwitch;
        public event Action OnLevelRestart;

        public void SwitchLevel(int levelID)
        {
            CacheThisLevelData();
            OnLevelSwitch?.Invoke();
            var levelName = _levelIndexer.GetLevelNameByIndex(levelID);
            SceneManager.LoadScene(levelName);
        }

        public void RestartLevel()
        {
            CacheThisLevelData();
            OnLevelRestart?.Invoke();
            var goalSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(goalSceneIndex);
        }

        private void Awake() => SceneManager.sceneLoaded += Init;
        private void OnDestroy() => SceneManager.sceneLoaded -= Init;

        private void Init(Scene scene, LoadSceneMode sceneMode)
        {
            SignalStart(scene);
        }

        private void SignalStart(Scene scene)
        {
            OnLevelStart?.Invoke();
        }
        
        private void CacheThisLevelData()
        {
            var current = SceneManager.GetActiveScene().name;
            PreviousLevel = _levelIndexer.GetLevelIndexByName(current);
        }
    }
}