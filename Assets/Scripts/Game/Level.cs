using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour 
{
    [SerializeField] private Transform downItemParent;
    [SerializeField] private PointsStorage upPointsStorage;

    private PointsStorage downPointsStorage;

    private List<PointsConnection> connections = new List<PointsConnection>();

    private int activatedPoints;

    public Action onAllPointedActivated;

    public void Init()
    {
        CopyPoints();
        CreateConnections();
    }

    private void CopyPoints()
    {
        downPointsStorage = Instantiate(upPointsStorage, downItemParent);
    }

    private void CreateConnections()
    {
        for (int i = 0; i < upPointsStorage.GetPointCount; i++)
        {
            var upPoint = upPointsStorage.GetPoint(i);
            var downPoint = downPointsStorage.GetPoint(i);

            var newConnection = new PointsConnection(upPoint, downPoint);
            newConnection.onActivatePoints += OnActivatePoints;

            connections.Add(newConnection);
        }
    }

    private void OnActivatePoints()
    {
        activatedPoints++;
        if (activatedPoints >= connections.Count)
        {
            onAllPointedActivated?.Invoke();
        }
    }

    private void OnDisable()
    {
        foreach (var connection in connections)
        {
            connection.onActivatePoints -= OnActivatePoints;
            connection.Dispose();
        }
    }
}