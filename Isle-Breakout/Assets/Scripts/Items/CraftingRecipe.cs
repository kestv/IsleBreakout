using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Item
{
    public GameObject requiredItem;
    [Range(1, 100)]
    public int count;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField] private List<Item> materials;    //Materials required to craft the item
    [SerializeField] private GameObject craftedItem;  //Item being crafted

    public bool CanCraft(IItemContainer itemContainer)
    {
        foreach(Item item in materials)
        {
            if(itemContainer.ItemCount(item.requiredItem.GetComponent<ItemSettings>().getName()) < item.count)
            {
                return false;
            }
        }
        return true;
    }

    public GameObject Craft(IItemContainer itemContainer)
    {
        if (CanCraft(itemContainer))
        {
            foreach(Item item in materials)
            {
                for (int i = 0; i < item.count; i++)
                {
                    Debug.Log("Calls remove");
                    itemContainer.RemoveItem(item.requiredItem.GetComponent<ItemSettings>().getName());
                }
            }
            Debug.Log("Craft success");
            return craftedItem;
        }
        return null;
    }

    public GameObject getCraftedItem()
    { return craftedItem; }

    public void setCraftedItem(GameObject craftedItem)
    { this.craftedItem = craftedItem; }

    public List<Item> getMaterials()
    { return materials; }

    public void setMaterials(List<Item> materials)
    { this.materials = materials; }
}
