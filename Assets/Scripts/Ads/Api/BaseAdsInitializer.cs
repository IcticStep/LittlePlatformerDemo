namespace Ads.Api
{
    public abstract class BaseAdsInitializer : IAdsInitializer
    {
        protected BaseAdsInitializer()
        {
            InitializeAds();
        }

        public abstract void InitializeAds();
    }
}