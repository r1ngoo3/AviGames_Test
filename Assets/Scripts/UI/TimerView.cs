using TMPro;
using UnityEngine;
using Zenject;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private PlayTimer timer;

    [Inject]
    public void Construct(PlayTimer timer)
    {
        this.timer = timer;

        timer.onTimerValueChange += OnTimerValueChanged;
    }

    private void OnTimerValueChanged()
    {
        float min = Mathf.FloorToInt(timer.CurrentTime / 60);
        float sec = Mathf.FloorToInt(timer.CurrentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
