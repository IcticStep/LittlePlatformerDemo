#if UNITY_WEBGL
using Ads.Api;
using UnityEngine;

namespace Ads.WebGLAds
{
    public class WebGLAdsInitializer : BaseAdsInitializer
    {
        public override void InitializeAds()
        {
            Debug.Log("WebGL ADS INITIALIZED");
        }
    }
}
#endif