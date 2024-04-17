using System;

public class PointsConnection: IDisposable
{
    private DistinctionPoint[] connectPoints;

    public Action onActivatePoints;

    public PointsConnection(params DistinctionPoint[] points)
    {
        connectPoints = points;

        foreach (var point in connectPoints)
        {
            point.onPointerClick += ActivatePoints;
        }
    }

    private void ActivatePoints()
    {
        foreach (var point in connectPoints)
        {
            point.ActivatePoint();
        }

        onActivatePoints?.Invoke();
    }

    public void Dispose()
    {
        foreach (var point in connectPoints)
        {
            point.onPointerClick -= ActivatePoints;
        }
    }
}