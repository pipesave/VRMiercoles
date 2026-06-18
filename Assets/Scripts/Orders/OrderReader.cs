using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderReader : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private List<Potion> potions = new List<Potion>(MAX_ORDER_SIZE);

    public List<Potion> Potions => potions;

    public Action<OrderReader, List<Potion>> onOrderUpdated;

    private const int MAX_ORDER_SIZE = 8;

    public void ClearOrder()
    {
        for (int i = 0; i < potions.Count; i++)
        {
            Destroy(potions[i].gameObject);
        }

        potions.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Potion potion))
        {
            potions.Add(potion);
            onOrderUpdated?.Invoke(this, potions);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Potion potion))
        {
            potions.Remove(potion);
            onOrderUpdated?.Invoke(this, potions);
        }
    }
}