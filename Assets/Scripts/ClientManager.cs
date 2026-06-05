using System.Collections.Generic;
using UnityEngine;
using System;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private List<RowPath> paths = new List<RowPath>();
    [SerializeField] private List<Transform> leavePath = new List<Transform>();

    public class Path
    {
        public List<Transform> Points { get; private set; }

        public Transform Target => Points[^1];
        public bool IsEmpty => Points.Count < 0;

        public Path(List<Transform> points)
        {
            Points = points;
        }
    }

    public Path GetLeavePath() => new Path(leavePath);

    public void SpawnClient()
    {
        Instantiate(clientPrefab, spawnpoint.position, Quaternion.identity);
    }

    public void OnOrderUpdated(OrderReader reader, List<Potion> potions)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i].orderReader == reader)
            {
                if (paths[i].waitingRow.OrderFinished(potions))
                {
                    paths[i].orderReader.ClearOrder();
                    break;
                }
            }
        }
    }

    public bool IsFirstInRow(Client client)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            if (paths[i].waitingRow.IsFirstInRow(client))
            {
                return true;
            }
        }

        return false;
    }

    public void FreePosition(Client client)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            paths[i].waitingRow.FreePosition(client);
        }
    }

    public Path GetPath(Client client)
    {
        List<Transform> points = new List<Transform>(8);
        RowPath lessOccupiedPath = default;
        bool foundPath = false;
        int freeSpots = 0;

        for (int i = 0; i < paths.Count; i++)
        {
            int freeSpotsTemp = paths[i].waitingRow.GetFreePositionsAmount();

            if (freeSpotsTemp > freeSpots)
            {
                lessOccupiedPath = paths[i];
                freeSpots = freeSpotsTemp;
                foundPath = true;
            }
        }

        if (foundPath)
        {
            points.Add(lessOccupiedPath.turningPoint);
            points.Add(lessOccupiedPath.waitingRow.ClaimFreePosition(client));
        }

        return new Path(points);
    }

    private void Awake()
    {
        for (int i = 0; i < paths.Count; i++)
        {
            paths[i].orderReader.onOrderUpdated += OnOrderUpdated;
        }
    }

    [Serializable]
    private struct RowPath
    {
        public OrderReader orderReader;
        public Transform turningPoint;
        public WaitingRow waitingRow;
    }
}