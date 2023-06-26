using System;
using Ads.Api;
using UnityEngine;

namespace Ads.None
{
    public class InterstitialNoneAds : IInterstitialAdShower
    {
        public event Action OnAdShowCompleted;
        public void Show()
        {
            Debug.Log("Ad could be shown here.");
            OnAdShowCompleted?.Invoke();
        }
    }
}
