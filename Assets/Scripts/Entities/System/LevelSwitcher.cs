using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Data;
using Entities.System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities.System
{
    public class LevelSwitcher : MonoBehaviour
    {
        private const string LevelNamePrefix = "Level";
        public PreviousLevel PreviousLevel { get; private set; }

        public event Action OnLevelStart;
        public event Action OnLevelSwitch;
        public event Action OnLevelRestart;

        private readonly Dictionary<EdgeAction, Action<EdgeSettings>> _edgeActions = new();
        
        public IReadOnlyList<EdgeSettings> EdgeSettings { set; private get; }

        private void Awake() => SceneManager.sceneLoaded += Init;
        private void OnDestroy() => SceneManager.sceneLoaded -= Init;

        private void Init(Scene scene, LoadSceneMode sceneMode)
        {
            InitEdgeActions();
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
            SceneManager.LoadScene(GetLevelNameByIndex(edgeSettings.GoalSceneIndex));
        }

        private void RestartLevel(EdgeSettings edgeSettings)
        {
            SetPreviousLevelData();
            OnLevelRestart?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void SetPreviousLevelData(Edge? edge = null)
            => PreviousLevel = new (SceneManager.GetActiveScene().buildIndex, edge);

        private string GetLevelNameByIndex(int index) => LevelNamePrefix + Convert.ToString(index);
    }
}