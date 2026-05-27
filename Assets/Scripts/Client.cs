using System;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private PathFollower pathFollower;
    [SerializeField] private Order order;

    private ClientManager clientPositionAssigner;

    public void MoveToNextPositionInRow(ClientManager.Path path)
    {
        SetPath(path, OnCounterReached);
    }

    public bool FullfilledOrder(List<Potion> potions)
    {
        bool result = order.IsOrder(potions);
        if (result) Destroy(gameObject);
        return result;
    }

    private void Awake()
    {
        clientPositionAssigner = FindFirstObjectByType<ClientManager>();
    }

    private void Start()
    {
        SetPath(clientPositionAssigner.GetPath(this), OnCounterReached);
    }

    private void OnDestroy()
    {
        clientPositionAssigner.FreePosition(this);
    }

    private void SetPath(ClientManager.Path path, Action callback)
    {
        pathFollower.SetPath(path);
        pathFollower.OnPathCompleted(callback);
    }

    private void OnCounterReached()
    {
        Debug.Log($"First in row: {clientPositionAssigner.IsFirstInRow(this)}");
    }
}