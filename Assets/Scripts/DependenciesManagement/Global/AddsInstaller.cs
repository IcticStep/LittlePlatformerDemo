using Ads;
using UnityEngine;
using Zenject;

namespace DependenciesManagement.Global
{
    public class AddsInstaller : MonoInstaller
    {
        [SerializeField] private InterstitialAds _interstitialAddShower;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container
                .Bind<IInterstitialAddShower>()
                .To<InterstitialAds>()
                .FromInstance(_interstitialAddShower)
                .AsSingle()
                .NonLazy();
        }
    }
}