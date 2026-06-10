using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "New Order", order = 0)]
public class Order : ScriptableObject
{
    [SerializeField] private List<Recipe> recipes;
    [field: SerializeField] public int Score { get; private set; } = 10;

    public List<Recipe> Recipes => recipes;

    public bool IsOrder(List<Recipe> recipes)
    {
        for (int i = 0; i < this.recipes.Count; i++)
        {
            if (recipes.Contains(this.recipes[i]))
            {
                recipes.Remove(this.recipes[i]);
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public bool IsOrder(List<Potion> potions)
    {
        if (potions.Count != this.recipes.Count)
        {
            return false;
        }

        List<Recipe> recipes = new List<Recipe>(potions.Count);

        for (int i = 0; i < potions.Count; i++)
        {
            recipes.Add(potions[i].Recipe);
        }

        return IsOrder(recipes);
    }
}