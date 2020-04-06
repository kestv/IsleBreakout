using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilder : MonoBehaviour
{
    public List<CraftingRecipe> recipes_1;
    public List<CraftingRecipe> recipes_2;
    public List<CraftingRecipe> recipes_3;

    public GameObject tier_1;
    public GameObject tier_2;
    public GameObject tier_3;

    public List<CraftingRecipe> currentRecipe;
    public GameObject currentTier;

    private void Start()
    {
        tier_1 = transform.GetChild(0).gameObject;
        tier_2 = transform.GetChild(1).gameObject;
        tier_3 = transform.GetChild(2).gameObject;

        currentRecipe = recipes_1;
        currentTier = tier_1;
    }

    public void BuildPart(GameObject go)
    {
        GameObject part = Instantiate(go, currentTier.transform);
    }

    public void RemoveRecipe(CraftingRecipe recipe)
    {
        currentRecipe.Remove(recipe);
    }

    public List<CraftingRecipe> GetActiveRecipes()
    {
        if(recipes_1.Count > 0)
        {
            return recipes_1;
        }
        else if (recipes_2.Count > 0)
        {
            currentRecipe = recipes_2;
            currentTier = tier_2;
            return recipes_2;
        }
        else if (recipes_3.Count > 0)
        {
            currentRecipe = recipes_3;
            currentTier = tier_3;
            return recipes_3;
        }

        return null;
    }




}
