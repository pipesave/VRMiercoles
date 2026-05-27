using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    private Recipe currentRecipe;

    public Recipe Recipe => currentRecipe;

    public void SetRecipe(Recipe recipe)
    {
        currentRecipe = recipe;
        meshFilter.mesh = recipe.Mesh;
    }

    private void Reset()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cauldron cauldron))
        {
            Recipe recipe = cauldron.GetRecipe();

            if (recipe != null)
            {
                SetRecipe(recipe);
            }
        }
    }
}