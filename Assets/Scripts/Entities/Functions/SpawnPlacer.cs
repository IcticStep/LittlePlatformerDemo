using ClassExtensions;
using DependenciesManagement;
using Entities.System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace Entities.Functions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpawnPlacer : MonoBehaviour
    {
        private readonly LevelIndexer _levelIndexer = new();
        private DoorContainer _doorContainer;
        private ILevelSwitcher _levelSwitcher;
        private Rigidbody2D _rigidbody;
        private Vector2 _startLevelPosition;
        private Vector2 _defaultSpawn;
        
        private bool _disabledForOneLevel;
        private bool _afterRestart;
        
        private int PreviousLevel => _levelSwitcher.GetPreviousLevel();

        [Inject]
        public void Construct(ILevelSwitcher levelSwitcher, Camera camera) => _levelSwitcher = levelSwitcher;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        private void OnEnable()
        {
            _levelSwitcher.OnLevelStart += SetSpawn;
            _levelSwitcher.OnLevelSwitch += ForgetDefaultSpawn;
            _levelSwitcher.OnLevelRestart += CacheRestart;
        }

        private void OnDisable()
        {
            _levelSwitcher.OnLevelStart -= SetSpawn;
            _levelSwitcher.OnLevelSwitch -= ForgetDefaultSpawn;
            _levelSwitcher.OnLevelRestart -= CacheRestart;
        }

        public void SetDoorContainer(DoorContainer doorContainer) => _doorContainer = doorContainer;

        public void SetDefaultSpawn(Vector2 position) => _defaultSpawn = position;

        public void DisableForOneLevelSwitch() => _disabledForOneLevel = true;

        private void ForgetDefaultSpawn() => _defaultSpawn = default;

        private void CacheRestart() => _afterRestart = true;

        private void SetSpawn()
        {
            if (_afterRestart)
            {
                _rigidbody.velocity = Vector2.zero;
                transform.position = _defaultSpawn;
                _afterRestart = false;
                return;
            }

            var levelSwitched = PreviousLevel != 0 && PreviousLevel != GetActiveSceneIndex();
            if (!levelSwitched)
                return;
            
            if (_disabledForOneLevel)
            {
                _disabledForOneLevel = false;
                return;
            }
                
            SetToDoorIfPossible();
        }

        private int GetActiveSceneIndex()
            => _levelIndexer.GetLevelIndexByName(SceneManager.GetActiveScene().name);

        // ReSharper disable once PossibleInvalidOperationException
        private void SetToDoorIfPossible()
        {
            if(!_doorContainer.HasDoorToLevel(PreviousLevel))
                return;
            
            var goalDoor = _doorContainer.GetDoorToLevel(PreviousLevel);
            transform.position = goalDoor.transform.position;
        }
    }
}