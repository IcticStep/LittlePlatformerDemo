using Ads;
using UnityEngine;
using Zenject;

namespace DependenciesManagement.Global
{
    public class AddsInstaller : MonoInstaller
    {
        [SerializeField] private IInterstitialAddShower _interstitialAddShower;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container
                .Bind<IInterstitialAddShower>()
                .FromInstance(_interstitialAddShower)
                .NonLazy();
        }
    }
}