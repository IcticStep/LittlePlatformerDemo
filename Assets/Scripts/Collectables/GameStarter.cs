using Entities.System;
using Zenject;

namespace Collectables
{
    internal sealed class GameStarter : CollectableItem
    {
        private LevelSwitcher _levelSwitcher;

        [Inject]
        private void Construct(LevelSwitcher levelSwitcher) =>
            _levelSwitcher = levelSwitcher;
        
        protected override void OnCollection() => 
            _levelSwitcher.SwitchLevel(1);
    }
}