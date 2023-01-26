using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IInterstitialAddShower
    {
        [SerializeField] private string _androidAdUnitId = "Interstitial_Android";
        [SerializeField] private string _iOsAdUnitId = "Interstitial_iOS";

        private string _adID;

        private void Awake()
        {
            InitializeAdId();
            Load();
        }

        private void InitializeAdId()
        {
            _adID = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOsAdUnitId
                : _androidAdUnitId;
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
            throw new NotImplementedException();
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Load();
        }
    }
}