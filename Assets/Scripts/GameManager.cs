using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private ClientManager clientManager;
    [SerializeField] private Canvas worldCanvas;

    [Space]

    [SerializeField] private int maxFails = 5;
    [SerializeField] private float clientSpawnCooldown = 10f;

    public static Canvas WorldCanvas => Instance.worldCanvas;

    private float spawnClientTimer = 0f;

    private int fails = 0;

    public static void IncreaseFails()
    {
        Instance.fails += 1;
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