using System;
using Entities.System;
using UnityEngine;
using Zenject;

namespace Collectables
{
    internal sealed class GameStarter : CollectableItem, IDisposable
    {
        private ILevelSwitcher _levelSwitcher;

        [Inject]
        private void Construct(ILevelSwitcher levelSwitcher) => 
            _levelSwitcher = levelSwitcher;

        protected override void Init() =>
            OnCollected += StartGame;

        public void Dispose() =>
            OnCollected -= StartGame;

        private void StartGame()
        {
            Debug.Log("Collided with game starter!");
            _levelSwitcher.SwitchLevel(1);
        }
    }
}