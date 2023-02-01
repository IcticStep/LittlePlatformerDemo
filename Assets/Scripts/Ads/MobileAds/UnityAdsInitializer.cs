#if (UNITY_IOS || UNITY_ANDROID)
using Ads.Api;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads.MobileAds
{
    public class UnityAdsInitializer : BaseAdsInitializer, IUnityAdsInitializationListener
    {
        private const string AndroidGameId = "5137069";
        private const string IOSGameId = "5137068";
        private const bool TestMode = true;
        
        private string _gameId;

        public override void InitializeAds()
        {
            _gameId = GetApplicationID();
            Advertisement.Initialize(_gameId, TestMode, this);
        }

        private string GetApplicationID()
        {
#if UNITY_ANDROID
            return AndroidGameId;
#elif UNITY_IOS
            return IOSGameId;
#endif
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}

#endif