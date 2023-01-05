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
            BindLevelSwitchSettings();
            LoadLevelConfiguration();
        }

        private void BindLevelSwitchSettings() => Container
                .Bind<List<EdgeSettings>>()
                .FromInstance(_levelSwitchConfiguration.EdgeSettings);

        private void LoadLevelConfiguration()
        {
            var levelConfigurationLoader = Container.InstantiateComponent<LevelConfigurationLoader>(_levelSwitcher.gameObject);
            _levelSwitcher.EdgeSettings = levelConfigurationLoader.LevelSettings;
            Destroy(levelConfigurationLoader);
        }
    }
}