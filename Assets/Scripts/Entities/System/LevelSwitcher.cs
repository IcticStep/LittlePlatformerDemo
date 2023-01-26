using System;
using System.Collections.Generic;
using System.Linq;
using Ads;
using Entities.Data;
using Entities.System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Entities.System
{
    public class LevelSwitcher : MonoBehaviour
    {
        private const string LevelNamePrefix = "Level";
        public PreviousLevel PreviousLevel { get; private set; }
        public IReadOnlyList<EdgeSettings> EdgeSettings { set; private get; }

        public event Action OnLevelStart;
        public event Action OnLevelSwitch;
        public event Action OnLevelRestart;

        private readonly Dictionary<EdgeAction, Action<EdgeSettings>> _edgeActions = new();
        private IInterstitialAddShower _interstitialAddShower;
        private int _restartSceneIndex;

        [Inject]
        private void Construct(IInterstitialAddShower interstitialAddShower)
        {
            _interstitialAddShower = interstitialAddShower;
        }

        private void Awake() => SceneManager.sceneLoaded += Init;

        private void Start()
        {
            _interstitialAddShower.OnUnityAdsShowCompleted += FinishLevelRestart;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= Init;
            _interstitialAddShower.OnUnityAdsShowCompleted -= FinishLevelRestart;
        }

        private void Init(Scene scene, LoadSceneMode sceneMode)
        {
            InitEdgeActions();
            SignalStart(scene);
        }

        private void SignalStart(Scene scene)
        {
            OnLevelStart?.Invoke();
        }

        private void InitEdgeActions()
        {
            _edgeActions[EdgeAction.Kill] = RestartLevel;
            _edgeActions[EdgeAction.SwitchLevel] = SwitchLevel;
        }

        public void FinishLevel(Edge edge)
        {
            var completed = EdgeSettings.Where(s => s.Edge == edge);
            if (!completed.Any())
                return;
            
            foreach (var edgeSettings in completed)
                if(_edgeActions.ContainsKey(edgeSettings.Action))
                    _edgeActions[edgeSettings.Action].Invoke(edgeSettings);
        }

        private void SwitchLevel(EdgeSettings edgeSettings)
        {
            SetPreviousLevelData(edgeSettings.Edge);
            OnLevelSwitch?.Invoke();
            var levelName = GetLevelNameByIndex(edgeSettings.GoalSceneIndex);
            SceneManager.LoadScene(levelName);
        }

        private void RestartLevel(EdgeSettings edgeSettings)
        {
            SetPreviousLevelData();
            OnLevelRestart?.Invoke();
            var goalSceneIndex = SceneManager.GetActiveScene().buildIndex;
            _restartSceneIndex = goalSceneIndex;
            _interstitialAddShower.Show();
        }
        
        private void FinishLevelRestart()
            => SceneManager.LoadScene(_restartSceneIndex);

        private void SetPreviousLevelData(Edge? edge = null)
            => PreviousLevel = new (SceneManager.GetActiveScene().buildIndex, edge);

        private string GetLevelNameByIndex(int index) => LevelNamePrefix + Convert.ToString(index);
    }
}