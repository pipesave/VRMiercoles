using System.Collections.Generic;
using UnityEngine;

public class WaitingRow : MonoBehaviour
{
    [SerializeField] private List<Transform> positions;

    public int PositionsCount => positions.Count;

    private List<PositionData> rowPositions;

    public bool OrderFinished(List<Potion> potions)
    {
        if (rowPositions.Count == 0) return false;
        Debug.Log("updated order");
        return rowPositions[0].client.FullfilledOrder(potions);
    }

    public bool IsFirstInRow(Client client)
    {
        if (rowPositions.Count > 0)
        {
            return rowPositions[0].client == client;
        }

        return false;
    }

    public int GetFreePositionsAmount()
    {
        int amount = 0;

        for (int i = 0; i < rowPositions.Count; i++)
        {
            if (rowPositions[i].Free)
            {
                amount += 1;
            }
        }

        return amount;
    }

    public Transform ClaimFreePosition(Client client)
    {
        for (int i = 0; i < rowPositions.Count; i++)
        {
            if (rowPositions[i].Free)
            {
                rowPositions[i].client = client;
                return rowPositions[i].transform;
            }
        }

        return null;
    }

    public void FreePosition(Client client)
    {
        bool hasFreedPosition = false;

        List<PositionData> updatePositions = new List<PositionData>();

        for (int i = 0; i < rowPositions.Count; i++)
        {
            if (!hasFreedPosition)
            {
                if (rowPositions[i].client == client)
                {
                    rowPositions[i].client = null;
                    hasFreedPosition = true;
                }
            }
            else if (!rowPositions[i].Free)
            {
                updatePositions.Add(rowPositions[i]);
            }
        }

        if (!hasFreedPosition) return;

        List<Client> waitingClients = new List<Client>(updatePositions.Count);

        for (int i = 0; i < updatePositions.Count; i++)
        {
            waitingClients.Add(updatePositions[i].client);
            updatePositions[i].client = null;
        }

        for (int i = 0; i < waitingClients.Count; i++)
        {
            Transform position = ClaimFreePosition(waitingClients[i]);
            List<Transform> points = new List<Transform>() { position };

            waitingClients[i].MoveToNextPositionInRow(new ClientManager.Path(points));
        }

        //move clients towards counter
    }

    public bool IsFree()
    {
        for (int i = 0; i < rowPositions.Count; i++)
        {
            if (rowPositions[i].Free)
            {
                return true;
            }
        }

        return false;
    }

    private void Awake()
    {
        rowPositions = new List<PositionData>(positions.Count);

        for (int i = 0; i < positions.Count; i++)
        {
            rowPositions.Add(new PositionData(positions[i], i));
        }
    }

    private class PositionData
    {
        public Transform transform;
        public Client client = null;
        public readonly int index;

        public bool Free => client == null;

        public PositionData(Transform transform, int index)
        {
            this.transform = transform;
            this.index = index;
        }
    }
}