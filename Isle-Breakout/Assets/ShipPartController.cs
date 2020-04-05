using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartController : MonoBehaviour
{
    public CraftingRecipe recipe;
    public GameObject slotPrefab;

    public void InitSlots(CraftingRecipe recipe)
    {
        foreach(Transform child in transform.GetChild(0))
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

    public void setRecipe(CraftingRecipe recipe)
    { this.recipe = recipe; }
}
