using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entities.Data;
using Entities.Functions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities.System
{
    public class LevelSwitcher : MonoBehaviour
    {
        [SerializeField] private OffCameraDetector _detector;
        [SerializeField] private List<EdgeSettings> _edgeSettings;
        [SerializeField] private float _killRestartDelay = 0.5f;
        [SerializeField] private float _nextLevelDelay;

        public static (string Name, Edge? CrossedEdge)? PreviousLevel { get; private set; }

        public static event Action OnLevelStart;
        public static event Action OnLevelSwitch;
        public static event Action OnLevelRestart;

        private readonly Dictionary<EdgeAction, Action<EdgeSettings>> _edgeActions = new();

        private void Awake()
        {
            InitEdgeActions();
            _detector.OnEdgeLeft += Finish;
        }

        private void Start()
        {
            OnLevelStart?.Invoke();
        }

        private void OnDestroy()
        {
            _detector.OnEdgeLeft -= Finish;
        }

        private void InitEdgeActions()
        {
            _edgeActions[EdgeAction.Kill] = RestartLevel;
            _edgeActions[EdgeAction.SwitchLevel] = SwitchLevel;
        }

        private void Finish(Edge edge)
        {
            var completed = _edgeSettings.Where(s => s.Edge == edge).ToList();
            if (completed.Count == 0)
                return;

            foreach (var edgeSettings in completed)
                if(_edgeActions.ContainsKey(edgeSettings.Action))
                    _edgeActions[edgeSettings.Action].Invoke(edgeSettings);
        }

        private void SwitchLevel(EdgeSettings edgeSettings) =>
            DOTween.Sequence()
                .InsertCallback(_nextLevelDelay, () =>
                {
                    SetPreviousLevelData(edgeSettings.Edge);
                    OnLevelSwitch?.Invoke();
                    SceneManager.LoadScene(edgeSettings.GoalScene.name);
                });

        private void RestartLevel(EdgeSettings edgeSettings) =>
            DOTween.Sequence()
                .InsertCallback(_killRestartDelay, () =>
                {
                    SetPreviousLevelData();
                    OnLevelRestart?.Invoke();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                });

        private static void SetPreviousLevelData(Edge? edge = null)
            => PreviousLevel = new (SceneManager.GetActiveScene().name, edge);

        [Serializable]
        private struct EdgeSettings
        {
            public Edge Edge;
            public EdgeAction Action;
            [Tooltip("Used if Action is set to Kill")]
            public SceneAsset GoalScene;
        }

        private enum EdgeAction
        {
            None,
            Kill,
            SwitchLevel,
        };
    }
}