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
        LoadRecipe();
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

    public void RemoveRecipe(CraftingRecipe recipe, int index)
    {
        recipes.Remove(recipe);
        Destroy(transform.GetChild(index).gameObject);
    }

    public void LoadRecipe()
    {
        if (recipes != null)
        {
            //foreach (CraftingRecipe recipe in recipes)
            //{
            //    GameObject slot = Instantiate(slotPrefab);
            //    slot.transform.SetParent(transform, false);
            //    ShipSlotController slotController = slot.GetComponent<ShipSlotController>();
            //    slotController.setRecipe(recipe);
            //}

            for (int i = 0; i < recipes.Count; i++)
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.transform.SetParent(transform, false);
                ShipSlotController slotController = slot.GetComponent<ShipSlotController>();
                slotController.setRecipe(recipes[i]);
                slotController.setRecipeIndex(i);
            }
        }
    }

    public CraftingRecipe getRecipe(int index)
    { return recipes[index]; }

    public void setRecipe(List<CraftingRecipe> recipes)
    { this.recipes = recipes; }
}
