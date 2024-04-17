using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IAPIncreaseTimeButton : MonoBehaviour
{
    [SerializeField] private Button iapButton;

    private IAPManager iapManager;
    private PlayTimer playTimer;

    [Inject]
    public void Construct(IAPManager iapManager, PlayTimer playTimer)
    {
        this.iapManager = iapManager;
        this.playTimer = playTimer;

        iapButton.onClick.AddListener(OnShowIAP);
    }

    private void OnShowIAP()
    {
        iapManager.BuyProductID("timer_10_sec", RewardAfterIAP);
    }

    private void RewardAfterIAP()
    {
        playTimer.AddTime(10);
    }
}
