using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entities.Data;
using EntitiesFunctions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities
{
    public class LevelSwitcher : MonoBehaviour
    {
        [SerializeField] private OffCameraDetector _detector;

        [SerializeField] private List<EdgeSettings> _edgeSettings;
        [SerializeField] private float _killRestartDelay = 0.5f;
        [SerializeField] private float _nextLevelDelay;

        public static (string Name, Edge? CrossedEdge)? PreviousLevel { get; private set; }

        public static event Action OnLevelStart;
        public static event Action OnLevelFinish;
        public static event Action OnLevelRestart;

        private void Start()
        {
            OnLevelStart?.Invoke();
            _detector.OnEdgeLeft += Finish;
        }

        private void OnDestroy()
        {
            _detector.OnEdgeLeft -= Finish;
        }

        private void Finish(Edge edge)
        {
            var completed = _edgeSettings.Where(s => s.Edge == edge).ToList();
            if (completed.Count == 0)
                return;

            foreach (var edgeSettings in completed)
                PerformAction(edgeSettings);
        }

        private void PerformAction(EdgeSettings data)
        {
            switch (data.Action)
            {
                case EdgeAction.Kill:
                    RestartLevel();
                    return;
                case EdgeAction.SwitchLevel:
                    GoToNextLevel(data.GoalScene.name, data.Edge);
                    return;
                case EdgeAction.None:
                default:
                    return;
            }
        }

        private void GoToNextLevel(string level, Edge crossed) =>
            DOTween.Sequence()
                .InsertCallback(_nextLevelDelay, () =>
                {
                    SetPreviousLevelData(crossed);
                    OnLevelFinish?.Invoke();
                    SceneManager.LoadScene(level);
                });

        private void RestartLevel() =>
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