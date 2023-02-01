using Entities.Controls;
using Entities.Functions;
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
    }
}