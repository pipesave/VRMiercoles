using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipes", menuName = "New Recipes", order = 0)]
public class Recipes : ScriptableObject
{
    [field: SerializeField] public List<Recipe> RecipeList { get; private set; }

    public Recipe GetRecipe(List<string> ingredients)
    {
        foreach (Recipe recipe in RecipeList)
        {
            if (recipe.IsRecipe(ingredients))
            {
                return recipe;
            }
        }

        return null;
    }
}