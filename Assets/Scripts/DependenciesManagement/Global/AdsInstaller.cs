using System;
using Ads.Api;
using Ads.None;
using UnityEngine;
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
            Debug.Log("Don't have ads for this platform. Ads won't be shown.");
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
#else
                .To<NoneAdsInitializer>()
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
#else
                .To<InterstitialNoneAds>()
#endif
                .AsSingle()
                .NonLazy();
        }
    }
}