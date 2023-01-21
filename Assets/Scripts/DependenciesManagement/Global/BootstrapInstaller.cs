using Entities.Controls;
using Entities.Functions;
using Entities.System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace DependenciesManagement.Global
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Camera _mainCameraPrefab;
        [SerializeField] private LevelSwitcher _levelSwitcherPrefab;
        [SerializeField] private EventSystem _eventSystemPrefab;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            CreateEventSystem();
            
            BindCamera();
            BindLevelSwitcher();
            BindPlayer();
        }

        private void CreateEventSystem()
        {
            // var eventSystem = Container.InstantiatePrefabForComponent<EventSystem>(_eventSystemPrefab);
            // DontDestroyOnLoad(eventSystem);
        }

        private void BindCamera()
        {
            // var camera = Container.InstantiatePrefabForComponent<Camera>(_mainCameraPrefab, new Vector3(0,0,-10), default, null);
            // Container
            //     .Bind<Camera>()
            //     .FromInstance(camera)
            //     .AsSingle();
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