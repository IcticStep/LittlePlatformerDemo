using Entities.Controls;
using Entities.Functions;
using Entities.System;
using UnityEngine;
using Zenject;

namespace DependenciesManagement
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Camera _mainCameraPrefab;
        [SerializeField] private LevelSwitcher _levelSwitcherPrefab;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            BindCamera();
            BindPlayer();
            BindLevelSwitcher();
        }

        private void BindCamera()
        {
            var camera = Container.InstantiatePrefabForComponent<Camera>(_mainCameraPrefab, new Vector3(0,0,-10), default, null);
            Container
                .Bind<Camera>()
                .FromInstance(camera)
                .AsSingle();
        }

        private void BindLevelSwitcher()
        {
            var levelSwitcher = Container.InstantiatePrefabForComponent<LevelSwitcher>(_levelSwitcherPrefab);
            Container
                .Bind<LevelSwitcher>()
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
            
            var offCameraDetector = player.GetComponent<OffCameraDetector>();
            Container
                .Bind<OffCameraDetector>()
                .FromInstance(offCameraDetector);
        }
    }
}