using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private GameObject prefab;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}