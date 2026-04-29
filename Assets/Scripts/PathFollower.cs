using UnityEngine;
using static ClientPositionAssigner;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;

    private ClientPositionAssigner clientPositionAssigner;

    private Path currentPath = null;
    private int currentPathIndex = 0;

    private void Awake()
    {
        clientPositionAssigner = FindFirstObjectByType<ClientPositionAssigner>();
    }

    private void Start()
    {
        currentPath = clientPositionAssigner.GetPath();
    }

    private void Update()
    {
        if (currentPathIndex >= currentPath.Points.Count) return;

        Vector3 targetPostion = new Vector3(currentPath.Points[currentPathIndex].position.x, transform.position.y, currentPath.Points[currentPathIndex].position.z);
        Vector3 moveDirection = (targetPostion - transform.position).normalized;
        Vector3 moveStep = speed * Time.deltaTime * moveDirection;

        transform.position += moveStep;

        if (Vector3.Distance(targetPostion, transform.position) < minDistance)
        {
            currentPathIndex += 1;
        }
    }
}