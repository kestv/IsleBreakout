using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartController : MonoBehaviour
{
    //-----------------------VARIABLES-----------------------
    public DependencyManager manager;
    public PlayerInventory inventory;
    public ShipBuilder shipBuilder;

    public CraftingRecipe recipe;
    public GameObject slotPrefab;

    public ShipRecipeController shipRecipeCtrl;

    //---------------------UNITY METHODS---------------------
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();
        shipRecipeCtrl = transform.root.GetChild(0).GetChild(0).Find("UI_ShipRecipeScrollPanel").GetChild(0).GetComponent<ShipRecipeController>();
        shipBuilder = manager.getShipBuilder();
    }

    //------------------------METHODS------------------------
    public void InitSlots(CraftingRecipe recipe)
    {
        foreach (Transform child in transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }

        this.recipe = recipe;

        foreach (Item item in recipe.getMaterials())
        {
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(transform.GetChild(0), false);
            ShipPartSlotController slotController = slot.GetComponent<ShipPartSlotController>();
            slotController.setItem(item);
        }
    }

    public void Craft()
    {
        GameObject craftedItem = recipe.Craft(inventory);
        if (craftedItem)
        {
            shipBuilder.BuildPart(craftedItem);
            shipBuilder.RemoveRecipe(recipe);
            shipRecipeCtrl.RemoveRecipe(recipe);
            LoadStartingRecipe();

            manager.getPlayer().GetComponent<PlayerMovementController>().enabled = true;
            manager.getCanvas().GetComponent<Canvas>().enabled = true;
            transform.root.gameObject.SetActive(false);
        }
    }

    public void LoadStartingRecipe()
    {
        shipRecipeCtrl.transform.GetChild(0).GetComponent<ShipSlotController>().UpdateUI();
    }

    public void RefreshRecipes()
    {
        if (shipRecipeCtrl.transform.childCount < 1)
        {
            shipRecipeCtrl.setRecipes(shipBuilder.UpdateActiveRecipes());
            shipRecipeCtrl.InitRecipes();
        }
    }

    //------------------------GET/SET------------------------
    public CraftingRecipe getRecipe()
    { return recipe; }

    public void setRecipe(CraftingRecipe recipe)
    { this.recipe = recipe; }
}
