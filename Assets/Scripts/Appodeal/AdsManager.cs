using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AdsManager : IAppodealInitializationListener
{
    public Action onInterstitialClose;
    public Action onInterstitialOpen;

    public AdsManager()
    {
        Appodeal.setTesting(true);
        int adTypes = Appodeal.INTERSTITIAL;
        string appKey = "YOUR_APPODEAL_APP_KEY";
        Appodeal.initialize(appKey, adTypes, this);
    }

    public void onInitializationFinished(List<string> errors)
    {
    }

    public bool TryShowInterstitial()
    {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL) && Appodeal.canShow(Appodeal.INTERSTITIAL, "default") && !Appodeal.isPrecache(Appodeal.INTERSTITIAL))
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
            return true;
        }
        else
        {
            DebugShowInter();
            return true;
        }
    }

    private async Task DebugShowInter()
    {
        onInterstitialOpen?.Invoke();
        await Task.Delay(3000);
        onInterstitialClose?.Invoke();
    }
}