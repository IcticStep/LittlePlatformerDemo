using System;

namespace Ads
{
    public interface IInterstitialAddShower
    {
        public event Action OnUnityAdsShowCompleted;
        public void Show();
    }
}