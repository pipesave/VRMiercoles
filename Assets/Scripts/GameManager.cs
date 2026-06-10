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

    [SerializeField] private UnityEvent onGameOver;

    public static Canvas WorldCanvas => Instance.worldCanvas;

    private float spawnClientTimer = 0f;

    private int fails = 0;

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

    private void Start()
    {
        scoreTracker.ResetScore();
    }

    private void Update()
    {
        spawnClientTimer += Time.deltaTime;

        if (spawnClientTimer >= clientSpawnCooldown)
        {
            spawnClientTimer = 0f;
            clientManager.SpawnClient();
        }
    }
}