using Entities;
using UnityEngine;

namespace Spawners
{
    [RequireComponent(typeof(Player))]
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerSpawnsContainer _spawnsContainer;
        private Player _player;
        
        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            if(_spawnsContainer.ProperCondition is null)
                return;
            
            ApplySpawn(_spawnsContainer.ProperCondition);
        }

        private void ApplySpawn(SpawnCondition condition)
        {
            transform.position = condition.Spawn.position;
            _player.SetMirrored(condition.Mirror);
        }
    }
}