using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button nextButton;

    protected CoreLoopSystem coreLoopSystem;

    [Inject]
    public virtual void Construct(CoreLoopSystem coreLoopSystem)
    {
        nextButton.onClick.AddListener(OnNextButtonClick);
        this.coreLoopSystem = coreLoopSystem;
    }

    public virtual void OpenMenu()
    {
        panel.SetActive(true);
    }

    public virtual void CloseMenu()
    {
        panel.SetActive(false);
    }

    public virtual void OnNextButtonClick()
    {
        coreLoopSystem.NextButtonClick();
        CloseMenu();
    }
}