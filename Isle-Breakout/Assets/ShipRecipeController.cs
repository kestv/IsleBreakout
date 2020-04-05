using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRecipeController : MonoBehaviour
{
    public List<CraftingRecipe> recipes;
    public GameObject slotPrefab;

    private void Start()
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
