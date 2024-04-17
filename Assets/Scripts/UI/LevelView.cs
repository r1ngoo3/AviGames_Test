using TMPro;
using UnityEngine;
using Zenject;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private CoreLoopSystem coreLoopSystem;

    [Inject]
    public void Construct(CoreLoopSystem coreLoopSystem)
    {
        this.coreLoopSystem = coreLoopSystem;

        coreLoopSystem.onLevelCreated += ChangeLevelValue;
        ChangeLevelValue();
    }

    private void ChangeLevelValue()
    {
        int visibleLevelValue = coreLoopSystem.CurrentLevelValue + 1;
        levelText.text = "Level " + visibleLevelValue;
    }
}
