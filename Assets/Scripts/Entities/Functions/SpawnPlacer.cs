using System;
using ClassExtensions;
using Entities.Data;
using Entities.System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Edge = Entities.Data.Edge;

namespace Entities.Functions
{
    public class SpawnPlacer : MonoBehaviour
    {
        [SerializeField] private float _basicEdgeLift = 1f;
        [SerializeField] private float _topEdgeLift = 2.5f;

        private LevelSwitcher _levelSwitcher;
        private Vector2 _switchedPosition;
        private Vector2 _startLevelPosition;
        private bool _afterRestart;
        
        private PreviousLevel? PreviousLevel => _levelSwitcher.PreviousLevel;
        
        private void Awake() => SetSpawn();

        [Inject]
        public void Construct(LevelSwitcher levelSwitcher) => _levelSwitcher = levelSwitcher;

        private void DoRestartResetIfNeeded()
        {
            if (_afterRestart)
            {
                transform.position = _startLevelPosition;
                _afterRestart = false;
                return;
            }

            _startLevelPosition = transform.position;
        }

        private void OnEnable()
        {
            _levelSwitcher.OnLevelSwitch += SaveSwitchPosition;
            _levelSwitcher.OnLevelStart += DoRestartResetIfNeeded;
            _levelSwitcher.OnLevelStart += SetSpawn;
            _levelSwitcher.OnLevelRestart += MarkRestart;
        }
        private void OnDisable()
        {
            _levelSwitcher.OnLevelSwitch -= SaveSwitchPosition;
            _levelSwitcher.OnLevelStart -= DoRestartResetIfNeeded;
            _levelSwitcher.OnLevelStart -= SetSpawn;
            _levelSwitcher.OnLevelRestart -= MarkRestart;
        }

        private void MarkRestart() => _afterRestart = true;

        private void SaveSwitchPosition() => _switchedPosition = transform.position;

        private void SetSpawn()
        {
            if(!NeedPositionReplace())
                return;

            transform.position = GetSpawnCoordinates();
        }

        private bool NeedPositionReplace() =>
            PreviousLevel != null 
            && PreviousLevel.Value.Id != SceneManager.GetActiveScene().buildIndex;

        // ReSharper disable once PossibleInvalidOperationException
        private Vector2 GetSpawnCoordinates() => PreviousLevel.Value.Crossed switch
            {
                Edge.Left => _switchedPosition.ReflectX().WithAdjustedX(-_basicEdgeLift),
                Edge.Right => _switchedPosition.ReflectX().WithAdjustedX(_basicEdgeLift),
                Edge.Bottom => _switchedPosition.ReflectY().WithAdjustedY(-_basicEdgeLift),
                Edge.Top => _switchedPosition.ReflectY() + new Vector2(0, _topEdgeLift),
                _ => _switchedPosition
            };
    }
}