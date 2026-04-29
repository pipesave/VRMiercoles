using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Recipe", menuName="New Recipe", order = 0)]
public class Recipe : ScriptableObject
{
    [SerializeField] private List<string> ingredients;
    [SerializeField] private Shader shader; //template for the potion shader

    public Shader Shader => shader;

    public bool IsRecipe(List<string> ingredients)
    {
        if (ingredients.Count != this.ingredients.Count)
        {
            return false;
        }

        for (int i = 0; i < ingredients.Count; i++)
        {
            if (!this.ingredients.Contains(ingredients[i]))
            {
                return false;
            }
        }

        return true;
    }
}