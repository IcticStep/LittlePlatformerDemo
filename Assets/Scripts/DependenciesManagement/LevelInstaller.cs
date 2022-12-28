using System.Collections.Generic;
using Configurations;
using Entities.Controls;
using Entities.Functions;
using Entities.System;
using Entities.System.Data;
using UnityEngine;
using Zenject;

namespace DependenciesManagement
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelSwitchConfiguration _levelSwitchConfiguration;
        
        private BootstrapInstaller _bootstrapInstaller;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Init();
            
            BindLevelSwitchSettings();
        }

        private void Init() => _bootstrapInstaller = ProjectContext.Instance.GetComponent<BootstrapInstaller>();
        
        private void BindLevelSwitchSettings() => Container
                .Bind<List<EdgeSettings>>()
                .FromInstance(_levelSwitchConfiguration.EdgeSettings);
    }
}