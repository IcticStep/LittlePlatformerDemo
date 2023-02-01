#if UNITY_WEBGL
using System;
using Ads.Api;
using UnityEngine;

namespace Ads.WebGLAds
{
    public class InterstitialWebGLAds : IInterstitialAdShower
    {
        public event Action OnAdShowCompleted;
        public void Show()
        {
            Debug.Log("WebGL ADS show");
        }
    }
}
#endif