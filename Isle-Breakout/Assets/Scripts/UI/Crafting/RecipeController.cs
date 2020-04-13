using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeController : MonoBehaviour
{
    public DependencyManager manager;
    public List<CraftingRecipe> recipes;
    public PlayerInventory inventory;
    public GameObject slotPrefab;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();

        foreach(CraftingRecipe recipe in recipes)
        {
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(transform);
            RecipeSlotController slotController = slot.GetComponent<RecipeSlotController>();
            slotController.setRecipe(recipe);
        }
    }

    public void Open()
    {
        transform.parent.parent.gameObject.SetActive(true);
        RefreshRecipeAvailability();
        Debug.Log("OPEN");
    }

    public void Close()
    {
        transform.parent.parent.gameObject.SetActive(false);
        Debug.Log("CLOSE");
    }

    public void RefreshRecipeAvailability()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<RecipeSlotController>().SetRecipeAvailability();
        }        
    }
}
