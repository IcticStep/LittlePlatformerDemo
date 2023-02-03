#if (UNITY_IOS || UNITY_ANDROID)

using System;
using Ads.Api;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads.MobileAds
{
    public class InterstitialMobileAds : IUnityAdsLoadListener, IUnityAdsShowListener, IInterstitialAdShower
    {
        private string AndroidAdUnitId = "Interstitial_Android";
        private string IOsAdUnitId = "Interstitial_iOS";
        
        public event Action OnAdShowCompleted;

        private string _adID;

        public InterstitialMobileAds()
        {
            InitializeAdId();
            Load();
        }

        private void InitializeAdId()
        {
            _adID = GetAdID();
        }

        private string GetAdID()
        {
#if UNITY_ANDROID
            return AndroidAdUnitId;
#elif UNITY_IOS
            return IOsAdUnitId;
#endif
        }

        private void Load()
        {
            Advertisement.Load(_adID, this);
        }

        public void Show()
        {
            Advertisement.Show(_adID, this);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            OnAdShowCompleted?.Invoke();
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log("Unity add failed to load!");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log("Unity add show failed!");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log("Add started!");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log("Add was clicked!");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Load();
        }
    }
}

#endif