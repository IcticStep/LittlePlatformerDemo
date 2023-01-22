using Entities.Controls;
using Entities.Functions;
using Entities.System;
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