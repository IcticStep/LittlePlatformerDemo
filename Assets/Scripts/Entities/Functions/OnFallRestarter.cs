using System;
using Entities.Functions.Movers;
using Entities.System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Entities.Functions
{
    [RequireComponent(typeof(Mover))]
    public class OnFallRestarter : MonoBehaviour
    {
        [SerializeField] private float _restartYBoundary = -10f;
        [SerializeField] private double _updateDelaySeconds = 0.5f;
        
        private Mover _mover;
        private ILevelSwitcher _levelSwitcher;

        [Inject]
        private void Construct(ILevelSwitcher levelSwitcher) => _levelSwitcher = levelSwitcher; 
        private void Awake()
        {
            _mover = GetComponent<Mover>();
            Observable.Timer(TimeSpan.FromSeconds(_updateDelaySeconds))
                .Repeat()
                .Subscribe(_ =>
                    {
                        RestartIfNeeded();
                    }
                );
        }
        
        private void RestartIfNeeded()
        {
            var boundaryLeft = transform.position.y <= _restartYBoundary;
            if(!boundaryLeft)
                return;
            
            _levelSwitcher.RestartLevel();
        }
    }
}
