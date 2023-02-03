using System;

namespace Ads.Api
{
    public interface IInterstitialAdShower
    {
        public event Action OnAdShowCompleted;
        public void Show();
    }
}