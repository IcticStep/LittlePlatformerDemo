using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IInterstitialAddShower
    {
        [SerializeField] private string _androidAdUnitId = "Interstitial_Android";
        [SerializeField] private string _iOsAdUnitId = "Interstitial_iOS";
        
        public event Action OnUnityAdsShowCompleted;

        private string _adID;

        private void Awake()
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
            return _androidAdUnitId;
#elif UNITY_IOS
            return _iOsAdUnitId;
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
            OnUnityAdsShowCompleted?.Invoke();
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