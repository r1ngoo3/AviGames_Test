using UnityEngine;

public class PointsStorage : MonoBehaviour
{
    [SerializeField] private DistinctionPoint[] points;
    public int GetPointCount => points.Length;

    public DistinctionPoint GetPoint(int index) =>
        points[index];

    private void OnValidate()
    {
        points = GetComponentsInChildren<DistinctionPoint>();
    }
}