using System;
using System.Collections.Generic;
using Triggers;
using UnityEngine;

namespace Entities.Spawners
{
    public class PlayerSpawnsContainer : MonoBehaviour
    {
        [SerializeField] private List<SpawnCondition> _spawnConditions;

        [NonSerialized] public SpawnCondition ProperCondition;
        
        private void Awake()
        {
            FindProperCondition();
        }

        private void FindProperCondition()
        {
            foreach (var condition in _spawnConditions)
                if (condition.PreviousScene.name == LevelFinisher.PreviousSceneName)
                    ProperCondition = condition;
        }
    }
}