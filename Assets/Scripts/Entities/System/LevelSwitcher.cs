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
            SignalStart(scene);
        }

        private void SignalStart(Scene scene)
        {
            OnLevelStart?.Invoke();
            Debug.Log($"Level started. Current scene: {scene.name}");
        }

        private void InitEdgeActions()
        {
            _edgeActions[EdgeAction.Kill] = RestartLevel;
            _edgeActions[EdgeAction.SwitchLevel] = SwitchLevel;
        }

        public void FinishLevel(Edge edge)
        {
            Debug.Log($"Finish level requested by {edge.ToString()} edge.");
            var completed = EdgeSettings.Where(s => s.Edge == edge);
            if (!completed.Any())
                return;
            
            Debug.Log("Finish level request is processing...");

            foreach (var edgeSettings in completed)
                if(_edgeActions.ContainsKey(edgeSettings.Action))
                    _edgeActions[edgeSettings.Action].Invoke(edgeSettings);
        }

        private void SwitchLevel(EdgeSettings edgeSettings)
        {
            SetPreviousLevelData(edgeSettings.Edge);
            OnLevelSwitch?.Invoke();
            var levelName = GetLevelNameByIndex(edgeSettings.GoalSceneIndex);
            Debug.Log($"Level switch. Going to load scene with name {levelName}.");
            SceneManager.LoadScene(levelName);
        }

        private void RestartLevel(EdgeSettings edgeSettings)
        {
            SetPreviousLevelData();
            OnLevelRestart?.Invoke();
            var goalSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log($"Level restart. Going to load scene with build index {goalSceneIndex}.");
            SceneManager.LoadScene(goalSceneIndex);
        }

        private void SetPreviousLevelData(Edge? edge = null)
            => PreviousLevel = new (SceneManager.GetActiveScene().buildIndex, edge);

        private string GetLevelNameByIndex(int index) => LevelNamePrefix + Convert.ToString(index);
    }
}