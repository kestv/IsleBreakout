using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRecipeController : MonoBehaviour
{
    //-----------------------VARIABLES-----------------------
    [Header("Prefabs")]
    [SerializeField] private GameObject slotPrefab;

    private List<CraftingRecipe> recipes;
    private DependencyManager manager;
    private ShipBuilder shipBuilder;

    //---------------------UNITY METHODS---------------------
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        shipBuilder = manager.getShipBuilder();
        recipes = shipBuilder.UpdateActiveRecipes();
        InitRecipes();
    }

    //------------------------METHODS------------------------
    public void RemoveRecipe(CraftingRecipe recipe)
    {
        recipes.Remove(recipe);

        foreach (Transform child in transform)
        {
            if (child.GetComponent<ShipSlotController>().getRecipe() == recipe)
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

    //------------------------GET/SET------------------------
    public CraftingRecipe getRecipe(int index)
    { return recipes[index]; }

    public void setRecipe(CraftingRecipe recipe, int index)
    { this.recipes[index] = recipe; }

    public List<CraftingRecipe> getRecipes()
    { return recipes; }

    public void setRecipes(List<CraftingRecipe> recipes)
    { this.recipes = recipes; }
}
