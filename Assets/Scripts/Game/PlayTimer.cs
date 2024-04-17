using System;
using System.Threading.Tasks;

public class PlayTimer : IDisposable
{
    private const int MAX_TIME = 120;

    private bool timerActive;
    private int currentTime;

    public Action onTimerEnd;
    public Action onTimerValueChange;

    public int CurrentTime => currentTime;

    private async Task Tick()
    {
        while (true)
        {
            await Task.Delay(1000);

            if (!timerActive)
                return;

            currentTime -= 1;

            onTimerValueChange?.Invoke();

            if (currentTime <= 0)
            {
                StopTimer();
                onTimerEnd?.Invoke();
            }
        }
    }

    public void StartTimer()
    {
        timerActive = true;
        Tick();
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public void ResetTimer()
    {
        currentTime = MAX_TIME;
        onTimerValueChange?.Invoke();
    }

    public void AddTime(int value)
    {
        currentTime += value;
        onTimerValueChange?.Invoke();
    }

    public void Dispose()
    {
        StopTimer();
    }
}
