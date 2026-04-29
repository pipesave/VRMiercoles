using System;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField] private Recipes recipes;

    [Header("Debug")]
    [SerializeField] private Recipe currentRecipe;

    private readonly List<string> ingredients = new List<string>();

    private Action<List<string>> onRecipeUpdate;

    public Recipe GetRecipe()
    {
        return recipes.GetRecipe(ingredients);
    }

    public void AddIngredient(string ingredient)
    {
        ingredients.Add(ingredient);
        onRecipeUpdate?.Invoke(ingredients);
        currentRecipe = GetRecipe();
    }

    public void ClearIngredients()
    {
        ingredients.Clear();
        onRecipeUpdate?.Invoke(ingredients);
        currentRecipe = GetRecipe();
    }
}
