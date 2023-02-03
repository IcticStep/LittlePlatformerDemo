using System;
using System.Collections.Generic;
using System.Linq;
using Ads.Api;
using Entities.Data;
using Entities.System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Entities.System
{
    public class LevelSwitcher : MonoBehaviour, ILevelSwitcher
    {
        public int PreviousLevel { get; private set; }
        public int GetPreviousLevel() => PreviousLevel;

        public event Action OnLevelStart;
        public event Action OnLevelSwitch;
        public event Action OnLevelRestart;

        private readonly LevelIndexer _levelIndexer = new();
        private IInterstitialAdShower _interstitialAdShower;
        private int _restartSceneIndex;

        [Inject]
        private void Construct(IInterstitialAdShower interstitialAdShower)
        {
            _interstitialAdShower = interstitialAdShower;
        }

        private void Start() => _interstitialAdShower.OnAdShowCompleted += FinishLevelRestart;

        private void Awake() => SceneManager.sceneLoaded += Init;
        
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= Init;
            _interstitialAdShower.OnAdShowCompleted -= FinishLevelRestart;
        }

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
            _restartSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            _interstitialAdShower.Show();
        }
        
        private void FinishLevelRestart()
            => SceneManager.LoadScene(_restartSceneIndex);

        private void Init(Scene scene, LoadSceneMode sceneMode) => SignalStart(scene);

        private void SignalStart(Scene scene) => OnLevelStart?.Invoke();

        private void CacheThisLevelData()
        {
            var current = SceneManager.GetActiveScene().name;
            PreviousLevel = _levelIndexer.GetLevelIndexByName(current);
        }
    }
}