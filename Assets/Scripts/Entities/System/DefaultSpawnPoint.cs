using System;
using Entities.Controls;
using Entities.Functions;
using UnityEngine;
using Zenject;

namespace Entities.System
{
    public class DefaultSpawnPoint : MonoBehaviour
    {
        private Player _player;

        [Inject]
        private void Construct(Player player) => _player = player;

        private void Awake()
        {
            var spawnPlacer = _player.GetComponent<SpawnPlacer>();
            spawnPlacer.SetDefaultSpawn(transform.position);
        }
    }
}