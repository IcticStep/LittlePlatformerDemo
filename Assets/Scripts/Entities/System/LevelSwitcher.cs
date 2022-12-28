using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entities.Data;
using Entities.Functions;
using Entities.System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Entities.System
{
    public class LevelSwitcher : MonoBehaviour
    {
        public PreviousLevel PreviousLevel { get; private set; }

        public event Action OnLevelStart;
        public event Action OnLevelSwitch;
        public event Action OnLevelRestart;

        private readonly Dictionary<EdgeAction, Action<EdgeSettings>> _edgeActions = new();
        
        private OffCameraDetector _detector;
        private List<EdgeSettings> _edgeSettings;

        [Inject]
        private void Construct(OffCameraDetector detector, List<EdgeSettings> edgeSettings)
        {
            _detector = detector;
            _edgeSettings = edgeSettings;
        }
        
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
                .InsertCallback(0, () =>
                {
                    SetPreviousLevelData(edgeSettings.Edge);
                    OnLevelSwitch?.Invoke();
                    SceneManager.LoadScene(edgeSettings.GoalScene.name);
                });

        private void RestartLevel(EdgeSettings edgeSettings) =>
            DOTween.Sequence()
                .InsertCallback(0, () =>
                {
                    SetPreviousLevelData();
                    OnLevelRestart?.Invoke();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                });

        private void SetPreviousLevelData(Edge? edge = null)
            => PreviousLevel = new (SceneManager.GetActiveScene().buildIndex, edge);
    }
}