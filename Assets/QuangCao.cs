using System;
using GoogleMobileAds.Api;
using SingleApp;
using UnityEngine;

public class QuangCao : PersistentSingleton<QuangCao>
{
#if UNITY_ANDROID
    // Test ad unit ID: ca-app-pub-3940256099942544/3419835294
    private const string AD_UNIT_ID = "ca-app-pub-1359637303091082/6429165255";
    private const string BANNER_UNIT_ID = "ca-app-pub-1359637303091082/6429165255";
#elif UNITY_IPHONE
      private const string AD_UNIT_ID = "ca-app-pub-7030564084462348/3506276416";
    private const string BANNER_UNIT_ID = "ca-app-pub-7030564084462348/3332970305";
#else
    private const string AD_UNIT_ID = "unexpected_platform";
#endif

    public void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadInterstitialAd();
            
        });
    }

    private AppOpenAd ad;

    private bool isShowingAd = false;

    private DateTime loadTime;

    public bool IsAdAvailable
    {
        get
        {
            // TODO: Consider ad expiration
            return ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;
        }
    }
    
    private RewardedAd rewardedAd;

    private InterstitialAd interstitialAd;

    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(AD_UNIT_ID, adRequest,
        (InterstitialAd ad, LoadAdError error) =>
        {
            // if error is not null, the load request failed.
            if (error != null || ad == null)
            {
                Debug.LogError("interstitial ad failed to load an ad " +
                               "with error : " + error);
                return;
            }

            Debug.Log("Interstitial ad loaded with response : "
                      + ad.GetResponseInfo());

            interstitialAd = ad;
        });
    }


    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public void ShowAd(Action action)
    {
        if (GameDataManager.Instance.playerData.removeAds)
        {
            action?.Invoke();
            return;
        }
        
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
            RegisterReloadHandler(interstitialAd, action);
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            LoadInterstitialAd();
        }
    }

    private void RegisterReloadHandler(InterstitialAd ad, Action action)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            action?.Invoke();
            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }

    BannerView _bannerView;

    /// <summary>
    /// Creates a 320x50 banner at top of the screen.
    /// </summary>
    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(BANNER_UNIT_ID, AdSize.SmartBanner, AdPosition.Bottom);
    }


    /// <summary>
    /// listen to events the banner may raise.
    /// </summary>
    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            _bannerView.Show();
            Debug.Log("Banner view loaded an ad with response : "
                      + _bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                           + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () => { Debug.Log("Banner view recorded an impression."); };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () => { Debug.Log("Banner view was clicked."); };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () => { Debug.Log("Banner view full screen content opened."); };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () => { Debug.Log("Banner view full screen content closed."); };
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }

    public bool GetRewardAvailable()
    {
        return interstitialAd == null || !interstitialAd.CanShowAd();
    }


    public void PhatQuangCao(Action action = null)
    {
        ShowAd(action);
    }

    public void PhatQuangCaoInter()
    {
        ShowAd(null);
    }



    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Displayed app open ad");
        isShowingAd = true;
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args)
    {
        Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
            args.AdValue.CurrencyCode, args.AdValue.Value);
    }
}