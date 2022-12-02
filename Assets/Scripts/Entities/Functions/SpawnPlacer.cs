using ClassExtensions;
using Entities.System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Edge = Entities.Data.Edge;

namespace Entities.Functions
{
    public class SpawnPlacer : MonoBehaviour
    {
        [SerializeField] private float _topEdgeLiftUp = 1f;
        
        private static Vector2 _finishedPosition;
        
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

        private void SaveSwitchPosition() => _finishedPosition = transform.position;

        private void SetSpawn()
        {
            if(!NeedPositionReplace())
                return;

            transform.position = GetGoalCoordinated();
        }

        private bool NeedPositionReplace() =>
            _previousLevel != null 
            && _previousLevel.Value.Name != SceneManager.GetActiveScene().name;

        private Vector2 GetGoalCoordinated() => _previousLevel.Value.CrossedEdge switch
            {
                Edge.Left or Edge.Right => _finishedPosition.ReflectX(),
                Edge.Bottom => _finishedPosition.ReflectY(),
                Edge.Top => _finishedPosition.ReflectY() + new Vector2(0, _topEdgeLiftUp),
                _ => _finishedPosition
            };
    }
}