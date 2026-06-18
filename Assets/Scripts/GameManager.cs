using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private ScoreTracker scoreTracker;
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private Canvas worldCanvas;

    [Space]

    [SerializeField] private int maxFails = 5;
    [SerializeField] private float clientSpawnCooldown = 10f;

    [Space]

    [SerializeField] private float difficultyScaleRate = 0.1f;
    [SerializeField] private List<Order> startOrders;
    [SerializeField] private List<DifficultyPoolUpdate> updates;

    [Space]

    [SerializeField] private UnityEvent onGameOver;

    [Header("Don't touch")]

    public List<Order> ordersPool;
    public List<DifficultyPoolUpdate> addedUpdates = new List<DifficultyPoolUpdate>();
    public float currentDifficulty = 0f;

    public static Canvas WorldCanvas => Instance.worldCanvas;

    private float spawnClientTimer = 0f;

    private int fails = 0;

    [Serializable]
    public struct DifficultyPoolUpdate
    {
        public float difficulty;
        public List<Order> recipes;
    }

    public static Order GetRandomOrder()
    {
        return Instance.ordersPool[UnityEngine.Random.Range(0, Instance.ordersPool.Count - 1)];
    }

    public static void IncreaseFails()
    {
        if (Instance.fails >= Instance.maxFails) return;

        Instance.fails += 1;

        if (Instance.fails >= Instance.maxFails)
        {
            Instance.onGameOver.Invoke();
        }

        Debug.Log($"fails {Instance.fails} / {Instance.maxFails}");
    }

    private void Awake()
    {
        ordersPool.AddRange(startOrders);
    }

    private void Start()
    {
        scoreTracker.ResetScore();
    }

    private void Update()
    {
        spawnClientTimer += Time.deltaTime;

        clientSpawnCooldown *= 1 - (0.01f * Time.deltaTime);

        if (spawnClientTimer >= clientSpawnCooldown)
        {
            spawnClientTimer = 0f;
            clientManager.SpawnClient();
        }

        UpdateDifficulty();
    }

    private void UpdateDifficulty()
    {
        currentDifficulty += difficultyScaleRate * Time.deltaTime;

        for (int i = 0; i < updates.Count; i++)
        {
            if (updates[i].difficulty <=  currentDifficulty)
            {
                if (addedUpdates.Contains(updates[i])) continue;

                addedUpdates.Add(updates[i]);

                for (int x = 0; x < updates[i].recipes.Count; x++)
                {
                    ordersPool.Add(updates[i].recipes[x]);

                    Debug.Log($"added {updates[i].recipes[x].name} recipe to the pool");
                }
            }
        }
    }
}