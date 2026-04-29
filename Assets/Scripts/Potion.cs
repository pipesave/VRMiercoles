using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private Shader shader;
    private Recipe currentRecipe;

    public void SetRecipe(Recipe recipe)
    {
        currentRecipe = recipe;
        shader = recipe.Shader;
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