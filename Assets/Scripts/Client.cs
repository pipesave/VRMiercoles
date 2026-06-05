using System;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private PathFollower pathFollower;

    [Space]

    [SerializeField] private float waitTime = 15f;

    public float PatiencePercentage => Mathf.Clamp01(timer / waitTime);

    private ClientManager clientManager;

    private Order order;

    private ClientState state = ClientState.Idle;

    private float timer = 0f;

    private enum ClientState
    {
        Idle,
        ToCounter,
        Ordering,
        Leaving
    }

    public void SetOrder(Order order)
    {
        this.order = order;
    }

    public void MoveToNextPositionInRow(ClientManager.Path path)
    {
        SetPath(path, OnCounterReached);
    }

    public bool FullfilledOrder(List<Potion> potions)
    {
        bool result = order.IsOrder(potions);
        if (result) Leave();
        return result;
    }

    private void Awake()
    {
        clientManager = FindFirstObjectByType<ClientManager>();
    }

    private void Start()
    {
        SetPath(clientManager.GetPath(this), OnCounterReached);
        state = ClientState.ToCounter;
    }

    private void Update()
    {
        if (state != ClientState.Ordering) return;

        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            state = ClientState.Leaving;
            Leave();
        }
    }

    private void OnDestroy()
    {
        clientManager.FreePosition(this);
    }

    private void SetPath(ClientManager.Path path, Action callback)
    {
        pathFollower.SetPath(path);
        pathFollower.OnPathCompleted(callback);
    }

    private void OnCounterReached()
    {
        state = ClientState.Ordering;
    }

    private void Leave()
    {
        SetPath(clientManager.GetLeavePath(), OnLeft);
    }

    private void OnLeft()
    {
        Destroy(gameObject);
        GameManager.
    }
}