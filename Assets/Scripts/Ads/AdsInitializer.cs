#if (UNITY_IOS || UNITY_ANDROID)

using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] private string _androidGameId = "5137069";
        [SerializeField] private string _iOSGameId = "5137068";
        [SerializeField] private bool _testMode = true;
        
        private string _gameId;

        private void Awake()
        {
            InitializeAds();
        }

        private void InitializeAds()
        {
            _gameId = GetApplicationID();
            Advertisement.Initialize(_gameId, _testMode, this);
        }

        private string GetApplicationID()
        {
#if UNITY_ANDROID
            return _androidGameId;
#elif UNITY_IOS
            return _iOSGameId;
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