using System.Collections.Generic;
using UnityEngine;
using System;

public class ClientPositionAssigner : MonoBehaviour
{
    [SerializeField] private List<Path> paths = new List<Path>();

    private int currentPathIndex = 0;

    [Serializable]
    public class Path
    {
        [field: SerializeField] public List<Transform> Points { get; private set; }
    }

    public Path GetPath()
    {
        if (currentPathIndex >= paths.Count)
        {
            currentPathIndex = 0;
        }

        currentPathIndex += 1;
        return paths[currentPathIndex - 1];
    }
}