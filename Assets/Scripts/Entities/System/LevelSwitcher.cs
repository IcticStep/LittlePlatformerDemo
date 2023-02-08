using System;
using Ads.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Entities.System
{
    public class LevelSwitcher : MonoBehaviour, ILevelSwitcher
    {
        public int PreviousLevel { get; private set; }
        public int CurrentLevel { get; private set; }

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

        public int GetPreviousLevel() => PreviousLevel;
        public int GetCurrentLevel() => CurrentLevel;

        public void SwitchLevel(int levelID)
        {
            UpdateLevelCache();
            OnLevelSwitch?.Invoke();
            var levelName = _levelIndexer.GetLevelNameByIndex(levelID);
            
            SceneManager.LoadScene(levelName);
        }

        public void RestartLevel()
        {
            UpdateLevelCache();
            OnLevelRestart?.Invoke();
            _restartSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            _interstitialAdShower.Show();
        }

        private void Init(Scene scene, LoadSceneMode sceneMode)
        {
            CurrentLevel = GetCurrentLevelID();
            SignalStart(scene);
        }

        private void UpdateLevelCache() => PreviousLevel = GetCurrentLevelID();

        private void FinishLevelRestart() => SceneManager.LoadScene(_restartSceneIndex);

        private void SignalStart(Scene scene) => OnLevelStart?.Invoke();
        
        private int GetCurrentLevelID()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            return _levelIndexer.GetLevelIndexByName(currentScene);
        }
    }
}