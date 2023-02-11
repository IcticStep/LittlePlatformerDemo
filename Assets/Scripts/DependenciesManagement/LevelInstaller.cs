using System;
using Collectables;
using Entities.Controls;
using Entities.Functions;
using Entities.System.Savers;
using Entities.System.Savers.Data;
using Zenject;

namespace DependenciesManagement
{
    public class LevelInstaller : MonoInstaller
    {
        private Player _player;

        [Inject]
        public void Construct(Player player) => _player = player;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            BindDoorContainer();
            BindCollectablesContainer();
            BindSaver();
        }

        private void BindCollectablesContainer()
        {
            var collectablesContainer = FindObjectOfType<CollectablesContainer>();
            Container
                .Bind<CollectablesContainer>()
                .FromInstance(collectablesContainer)
                .AsSingle();
        }

        private void BindDoorContainer()
        {
            var doorContainer = new DoorContainer();
            Container
                .Bind<DoorContainer>()
                .FromInstance(doorContainer)
                .AsSingle();

            var playerPlacer = _player.GetComponent<SpawnPlacer>();
            playerPlacer.SetDoorContainer(doorContainer);
        }

        private void BindSaver()
        {
            Container
                .BindInterfacesAndSelfTo<SceneSaver>()
                .AsSingle()
                .WithConcreteId(SaveSettings.SceneSaverID)
                .NonLazy();
        }
    }
}