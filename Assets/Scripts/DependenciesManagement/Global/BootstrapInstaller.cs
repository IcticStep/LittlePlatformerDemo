using Entities.Controls;
using Entities.System;
using Entities.System.Savers;
using Entities.System.Savers.Data;
using UnityEngine;
using Zenject;

namespace DependenciesManagement.Global
{
    public class BootstrapInstaller : MonoInstaller
    {
        [Header("Children")]
        [SerializeField] private Camera _cameraChild;
        
        [Header("Prefabs")]
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private LevelSwitcher _levelSwitcherPrefab;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            BindCamera();
            BindLevelSwitcher();
            BindPlayer();
            BindSaver();
        }

        private void BindCamera()
        {
            Container
                .Bind<Camera>()
                .FromInstance(_cameraChild)
                .AsSingle();
        }

        private void BindLevelSwitcher()
        {
            var levelSwitcher = Container.InstantiatePrefabForComponent<LevelSwitcher>(_levelSwitcherPrefab);
            Container
                .Bind<ILevelSwitcher>()
                .To<LevelSwitcher>()
                .FromInstance(levelSwitcher)
                .AsSingle();
        }

        private void BindPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, default, default, null);
            Container
                .Bind<Player>()
                .FromInstance(player)
                .AsSingle();
        }
        
        private void BindSaver()
        {
            Container
                .BindInterfacesAndSelfTo<ProjectSaver>()
                .AsSingle()
                .WithConcreteId(SaveSettings.GlobalSaverID)
                .NonLazy();
        }
    }
}