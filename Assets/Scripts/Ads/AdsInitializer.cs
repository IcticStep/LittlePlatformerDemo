using System;
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
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOSGameId
                : _androidGameId;
            Advertisement.Initialize(_gameId, _testMode, this);
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