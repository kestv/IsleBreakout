using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRecipeController : MonoBehaviour
{
    public DependencyManager manager;
    public ShipBuilder shipBuilder;

    public List<CraftingRecipe> recipes;
    public GameObject slotPrefab;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        shipBuilder = manager.getShipBuilder();
        recipes = shipBuilder.GetActiveRecipes();
        InitRecipes();
    }

    public void RemoveRecipe(CraftingRecipe recipe)
    {
        recipes.Remove(recipe);

        foreach(Transform child in transform)
        {
            if(child.GetComponent<ShipSlotController>().getRecipe() == recipe)
            {
                Destroy(child.gameObject);
            }
        }        
    }

    public void InitRecipes()
    {
        if (recipes != null)
        {
            foreach (CraftingRecipe recipe in recipes)
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.transform.SetParent(transform, false);
                ShipSlotController slotController = slot.GetComponent<ShipSlotController>();
                slotController.setRecipe(recipe);
            }
        }
    }

    public CraftingRecipe getRecipe(int index)
    { return recipes[index]; }

    public void setRecipe(List<CraftingRecipe> recipes)
    { this.recipes = recipes; }
}
