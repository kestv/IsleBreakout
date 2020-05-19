using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilder : MonoBehaviour
{
    //-----------------------VARIABLES-----------------------
    [SerializeField] private List<CraftingRecipe> recipes_1;
    [SerializeField] private List<CraftingRecipe> recipes_2;
    [SerializeField] private List<CraftingRecipe> recipes_3;

    private GameObject tier_1;
    private GameObject tier_2;
    private GameObject tier_3;

    private List<CraftingRecipe> activeRecipes;
    private GameObject activeTier;

    //---------------------UNITY METHODS---------------------
    private void Start()
    {
        tier_1 = transform.GetChild(0).gameObject;
        tier_2 = transform.GetChild(1).gameObject;
        tier_3 = transform.GetChild(2).gameObject;

        activeRecipes = recipes_1;
        activeTier = tier_1;
    }

    //------------------------METHODS------------------------
    public void BuildPart(GameObject go)
    {
        GameObject part = Instantiate(go, activeTier.transform);
    }

    public void RemoveRecipe(CraftingRecipe recipe)
    {
        activeRecipes.Remove(recipe);
    }

    public List<CraftingRecipe> UpdateActiveRecipes()
    {
        if(recipes_1.Count > 0)
        {
            return recipes_1;
        }
        else if (recipes_2.Count > 0)
        {
            activeRecipes = recipes_2;
            activeTier = tier_2;
        }
        else if (recipes_3.Count > 0)
        {
            activeRecipes = recipes_3;
            activeTier = tier_3;
        }

        return activeRecipes;
    }

    //------------------------GET/SET------------------------
    public List<CraftingRecipe> getActiveRecipes()
    { return activeRecipes; }

    public void setActiveRecipes(List<CraftingRecipe> activeRecipes)
    { this.activeRecipes = activeRecipes; }

    public GameObject getActiveTier()
    { return activeTier; }

    public void setActiveTier(GameObject activeTier)
    { this.activeTier = activeTier; }
}
