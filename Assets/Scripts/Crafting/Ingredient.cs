using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private string id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cauldron cauldron))
        {
            cauldron.AddIngredient(id);
            Despawn();
        }
    }

    private void Despawn()
    {
        gameObject.SetActive(false);
    }
}