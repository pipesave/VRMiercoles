using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Recipe", menuName="New Recipe", order = 0)]
public class Recipe : ScriptableObject
{
    [SerializeField] private List<string> ingredients;
    [SerializeField] private Mesh mesh;

    public Mesh Mesh => mesh;

    public bool IsRecipe(List<string> ingredients)
    {
        if (ingredients.Count != this.ingredients.Count)
        {
            return false;
        }

        List<string> ingredientsCopy = new List<string>(ingredients);

        for (int i = 0; i < this.ingredients.Count; i++)
        {
            if (ingredientsCopy.Contains(this.ingredients[i]))
            {
                ingredientsCopy.Remove(this.ingredients[i]);
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}