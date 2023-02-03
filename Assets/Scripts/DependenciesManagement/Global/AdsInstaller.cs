using System;
using Ads.Api;
#if (UNITY_IOS || UNITY_ANDROID)
using Ads.MobileAds;
#elif UNITY_WEBGL
using Ads.WebGLAds;
#endif
using Zenject;

namespace DependenciesManagement.Global
{
    public class AdsInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
#if (!UNITY_IOS && !UNITY_ANDROID && !UNITY_WEBGL)
            throw new Exception("Don't have ads for this platform");
#endif
            BindAdInitializer();
            BindAdShower();
        }

        private void BindAdInitializer()
        {
              Container
                .Bind<IAdsInitializer>()
#if (UNITY_IOS || UNITY_ANDROID)
                .To<UnityAdsInitializer>()
#elif UNITY_WEBGL
                .To<WebGLAdsInitializer>()
#endif
                .AsSingle()
                .NonLazy();
        }

        private void BindAdShower()
        {
            Container
                .Bind<IInterstitialAdShower>()
#if (UNITY_IOS || UNITY_ANDROID)
                .To<InterstitialMobileAds>()
#elif UNITY_WEBGL
                .To<InterstitialWebGLAds>()
#endif
                .AsSingle()
                .NonLazy();
        }
    }
}