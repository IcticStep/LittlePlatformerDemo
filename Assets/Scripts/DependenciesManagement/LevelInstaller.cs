using System.Collections.Generic;
using Configurations;
using Entities.System;
using Entities.System.Data;
using UnityEngine;
using Zenject;

namespace DependenciesManagement
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelSwitchConfiguration _levelSwitchConfiguration;
        private LevelSwitcher _levelSwitcher;

        [Inject]
        public void Construct(LevelSwitcher levelSwitcher) => _levelSwitcher = levelSwitcher;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            LoadLevelConfiguration();
        }

        private void LoadLevelConfiguration() => _levelSwitcher.EdgeSettings = _levelSwitchConfiguration.EdgeSettings;
    }
}