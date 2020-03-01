using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerInventory : MonoBehaviour
{
    private List<GameObject> items;  //Item list
    private int maxItems;            //Maximal ammount of items in item list
    private bool inventoryFull;      //Is inventory full
    private int itemSelector;        //Active hotbar slot
    private int itemCount;           //Items in inventory count

    //public Canvas canvas;
    private DependencyManager dependencyMngr;
    private GameObject hotbar;

    void Start()
    {
        items = new List<GameObject>();
        maxItems = 3;

        for (int i = 0; i < maxItems; i++)
        {
            items.Add(null);
        }

        try
        {
            dependencyMngr = transform.GetComponent<DependencyManager>();
            hotbar = dependencyMngr.getHotbar();
        }
        catch (Exception e)
        {
            Debug.Log(this.GetType().Name + " " + e.ToString());
        }
    }

    #region Get/Set methods
    public List<GameObject> getItems()
    { return items; }

    public GameObject getItem(int index)
    { return items[index]; }

    public int getMaxItems()
    { return maxItems; }

    public void setMaxItems(int value)
    { maxItems = value; }

    public bool getInventoryFull()
    { return inventoryFull; }

    public void setInventoryFull(bool state)
    { inventoryFull = state; }

    public int getItemSelector()
    { return itemSelector; }

    public void setItemSelector(int value)
    { itemSelector = value; }

    public int getItemCount()
    { return itemCount; }

    public void setItemCount(int value)
    { itemCount = value; }
    #endregion

    public void AddItem(GameObject go)
    {
        if (go != null)
        {
            items.Add(go);
            IncreaseItemCount();
        }
    }

    public void InsertItem(GameObject go, int index)
    {
        if (go != null && index >= 0 && index < items.Count)
        {
            //Add item to items list (inventory)
            items[index] = go;
            IncreaseItemCount();

            //Change object to be a child of slot GameObject and disable rendering
            ItemParameters itemParams = go.GetComponent<ItemParameters>();
            itemParams.ChangeParent(hotbar.transform.GetChild(index));
            itemParams.Disable();

            //Update hotbar with image of the picked up item
            hotbar.transform.GetChild(index).GetChild(1).GetComponent<Image>().sprite = go.GetComponent<ItemParameters>().itemImage.sprite;
            hotbar.transform.GetChild(index).GetChild(1).gameObject.SetActive(true);
        }
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            ItemParameters itemParams = items[index].GetComponent<ItemParameters>();
            itemParams.Enable(transform.position);

            hotbar.transform.GetChild(index).GetChild(1).gameObject.SetActive(false);

            items[index] = null;
            DecreaseItemCount();
        }
    }

    public void RemoveItem(GameObject go)
    {
        if (go != null && items.Contains(go))
        {
            items.Remove(go);
            inventoryFull = false;
        }
    }

    public void PickUp(GameObject go)
    {
        if (items[itemSelector] == null)
        {
            InsertItem(go, itemSelector);
        }
        else if (!inventoryFull)
        {
            for (int i = 0; i < maxItems; i++)
            {
                if (items[i] == null)
                {
                    InsertItem(go, i);
                    break;
                }
            }
        }
    }

    public void Drop()
    {
        if (items[itemSelector] != null)
        {
            RemoveItem(itemSelector);
        }
    }

    public void IncreaseItemCount()
    {
        itemCount++;
        if (itemCount <= maxItems)
        {
            inventoryFull = false;
        }
        else
        {
            inventoryFull = true;
        }
    }

    public void DecreaseItemCount()
    {
        itemCount--;
        inventoryFull = false;
    }
}
