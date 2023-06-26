using Ads.Api;
using UnityEngine;

namespace Ads.None
{
    public class NoneAdsInitializer : BaseAdsInitializer
    {
        public override void InitializeAds()
        {
            Debug.Log("Initialized without ads.");
        }
    }
}
