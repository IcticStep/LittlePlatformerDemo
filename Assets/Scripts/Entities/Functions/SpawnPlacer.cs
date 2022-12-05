using ClassExtensions;
using Entities.System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Edge = Entities.Data.Edge;

namespace Entities.Functions
{
    public class SpawnPlacer : MonoBehaviour
    {
        [SerializeField] private float _basicEdgeLift = 1f;
        [SerializeField] private float _topEdgeLift = 2.5f;
        
        private static Vector2 _switchedPosition;
        
        private readonly (string Name, Edge? CrossedEdge)? _previousLevel = LevelSwitcher.PreviousLevel;

        private void Awake() => SetSpawn();

        private void OnEnable()
        {
            LevelSwitcher.OnLevelSwitch += SaveSwitchPosition;
            LevelSwitcher.OnLevelStart += SetSpawn;
        }
        private void OnDisable()
        {
            LevelSwitcher.OnLevelSwitch -= SaveSwitchPosition;
            LevelSwitcher.OnLevelStart -= SetSpawn;
        }

        private void SaveSwitchPosition() => _switchedPosition = transform.position;

        private void SetSpawn()
        {
            if(!NeedPositionReplace())
                return;

            transform.position = GetSpawnCoordinates();
        }

        private bool NeedPositionReplace() =>
            _previousLevel != null 
            && _previousLevel.Value.Name != SceneManager.GetActiveScene().name;

        // ReSharper disable once PossibleInvalidOperationException
        private Vector2 GetSpawnCoordinates() => _previousLevel.Value.CrossedEdge switch
            {
                Edge.Left => _switchedPosition.ReflectX().WithAdjustedX(-_basicEdgeLift),
                Edge.Right => _switchedPosition.ReflectX().WithAdjustedX(_basicEdgeLift),
                Edge.Bottom => _switchedPosition.ReflectY().WithAdjustedY(-_basicEdgeLift),
                Edge.Top => _switchedPosition.ReflectY() + new Vector2(0, _topEdgeLift),
                _ => _switchedPosition
            };
    }
}