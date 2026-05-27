using UnityEngine;
using System;
using static ClientManager;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;

    private Action onPathCompleted;

    private Path currentPath = null;
    private int currentPathIndex = 0;

    public void SetPath(Path path)
    {
        currentPath = path;
        currentPathIndex = 0;
    }

    public void OnPathCompleted(Action callback)
    {
        onPathCompleted = callback;
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

        if (currentPathIndex >= currentPath.Points.Count)
        {
            onPathCompleted?.Invoke();
            onPathCompleted = null;
        }
    }
}