using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

public class DebugAdsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI adsText;

    [Inject]
    public void Construct(AdsManager adsManager)
    {
        adsManager.onInterstitialOpen += OnShowInterstitial;
        adsManager.onInterstitialClose += OnCloseInterstitial;
    }

    private void OnShowInterstitial()
    {
        adsText.text = "Ads Show";
    }

    private async void OnCloseInterstitial()
    {
        adsText.text = "Ads Close";
        await Task.Delay(2000);
        adsText.text = "";
    }
}
