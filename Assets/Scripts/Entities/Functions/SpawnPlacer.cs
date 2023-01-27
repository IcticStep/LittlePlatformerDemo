using ClassExtensions;
using DependenciesManagement;
using Entities.System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Entities.Functions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpawnPlacer : MonoBehaviour
    {
        private readonly LevelIndexer _levelIndexer = new();
        private DoorContainer _doorContainer;
        private ILevelSwitcher _levelSwitcher;
        private Camera _camera;
        private Rigidbody2D _rigidbody;
        private Vector2 _startLevelPosition;
        private Vector2 _defaultSpawn;
        
        private int PreviousLevel => _levelSwitcher.GetPreviousLevel();
        
        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();
        private void Start() => SetSpawn();

        [Inject]
        public void Construct(ILevelSwitcher levelSwitcher, Camera camera)
        {
            _levelSwitcher = levelSwitcher;
            _camera = camera;
        }

        public void SetDoorContainer(DoorContainer doorContainer) => _doorContainer = doorContainer;

        private void OnEnable()
        {
            _levelSwitcher.OnLevelStart += SetSpawn;
            _levelSwitcher.OnLevelSwitch += ForgetDefaultSpawn;
        }

        private void OnDisable()
        {
            _levelSwitcher.OnLevelStart -= SetSpawn;
            _levelSwitcher.OnLevelSwitch -= ForgetDefaultSpawn;
        }

        public void SetDefaultSpawn(Vector2 position) => _defaultSpawn = position; 
        private void ForgetDefaultSpawn() => _defaultSpawn = default;
        
        private void SetSpawn()
        {
            var levelSwitched = PreviousLevel != 0 && PreviousLevel != GetActiveSceneIndex();
            
            if (levelSwitched)
            {
                transform.position = GetSpawnDoorCoordinates();
                return;
            }

            _rigidbody.velocity = Vector2.zero;
            transform.position = _defaultSpawn;
        }

        private int GetActiveSceneIndex()
            => _levelIndexer.GetLevelIndexByName(SceneManager.GetActiveScene().name);

        // ReSharper disable once PossibleInvalidOperationException
        private Vector2 GetSpawnDoorCoordinates()
        {
            var goalDoor = _doorContainer.GetDoorToLevel(PreviousLevel);
            return goalDoor.transform.position;
        }
    }
}