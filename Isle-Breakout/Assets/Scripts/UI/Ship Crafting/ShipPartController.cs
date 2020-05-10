using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartController : MonoBehaviour
{
    //-----------------------VARIABLES-----------------------
    [Header("Prefabs")]
    [SerializeField] private GameObject slotPrefab;

    private DependencyManager manager;
    private PlayerInventory inventory;
    private ShipBuilder shipBuilder;

    private CraftingRecipe recipe;
    private ShipRecipeController shipRecipeCtrl;

    //---------------------UNITY METHODS---------------------
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();

        shipRecipeCtrl = transform.root.GetChild(5).GetComponent<ShipRepair>().getRecipeScrollPanel().GetChild(0).GetComponent<ShipRecipeController>();
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

            transform.root.GetChild(5).gameObject.SetActive(false);

            Transform shipBuilderTransform = shipBuilder.transform;
            if (!shipBuilderTransform.GetChild(shipBuilderTransform.childCount - 1).GetChild(shipBuilderTransform.GetChild(shipBuilderTransform.childCount - 1).childCount - 1).gameObject.activeSelf)
            {
                manager.getPlayer().GetComponent<PlayerTriggerHandler>().UpdateTriggerMessage();
            }
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
        else
        {
            LoadStartingRecipe();
        }
    }

    //------------------------GET/SET------------------------
    public CraftingRecipe getRecipe()
    { return recipe; }

    public void setRecipe(CraftingRecipe recipe)
    { this.recipe = recipe; }
}
