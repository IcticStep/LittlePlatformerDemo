using Entities.Controls;
using Entities.Functions.DoorsSystem;
using Entities.System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Entities.Functions
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Collider2D))]
    public class OpenerDoor : MonoBehaviour
    {
        private readonly LevelIndexer _levelIndexer = new(); 
        private ILevelSwitcher _levelSwitcher;
        private Rigidbody2D _rigidbody;
        private Player _player;
        private int _justUsedDoorFrom;
        private bool _canForgetDoor;

        [Inject]
        private void Construct(ILevelSwitcher levelSwitcher) => _levelSwitcher = levelSwitcher;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _player = GetComponent<Player>();
            
            _levelSwitcher.OnLevelStart += Init;
        }

        private void OnDestroy()
        {
            _levelSwitcher.OnLevelStart -= Init;
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            var otherObject = otherCollider.gameObject; 
            var isDoor = otherObject.TryGetComponent<IDoor>(out var door);
            if(!isDoor) 
                return;
            
            if(door.GetGoalLevel() == _justUsedDoorFrom)
                return;

            _canForgetDoor = false;
            _justUsedDoorFrom = GetCurrentSceneIndex();
            door.Open();
            StopInDoor();
        }

        private void OnTriggerExit2D(Collider2D otherCollider)
        {
            var otherObject = otherCollider.gameObject; 
            var isDoor = otherObject.TryGetComponent<IDoor>(out var door);
            if(!isDoor) 
                return;

            if (!_canForgetDoor)
                return;

            _justUsedDoorFrom = default;
            _canForgetDoor = false;
        }

        private void Init()
        {
            _canForgetDoor = true;
        }

        private int GetCurrentSceneIndex()
        {
            var currentName = SceneManager.GetActiveScene().name;
            return _levelIndexer.GetLevelIndexByName(currentName);
        }
        
        private void StopInDoor()
        {
            _rigidbody.velocity = Vector2.zero;
            _player.DisableInput();
        }
    }
}