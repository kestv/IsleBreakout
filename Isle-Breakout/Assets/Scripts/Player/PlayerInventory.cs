using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IItemContainer
{
    private DependencyManager manager;
    private List<GameObject> inventory;
    private int inventorySize;
    private GameObject canvas;
    private GameObject inventoryPanel;
    private GameObject craftingPanel;
    private GameObject shipPartPanel;
    private bool inventoryFull;
    private int itemCount;
    private CanvasController canvasCtrl;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();        
        canvas = manager.getCanvas();
        canvasCtrl = canvas.GetComponent<CanvasController>();
        inventoryPanel = canvasCtrl.getInventoryPanel().gameObject;
        craftingPanel = canvasCtrl.getCraftingPanel().gameObject;
        shipPartPanel = manager.getShipRepairPanel().GetComponent<ShipRepair>().getRecipePartPanel().gameObject;

        inventorySize = manager.getInventorySize();
        inventory = new List<GameObject>();
        for (int i = 0; i < inventorySize; i++)
        {
            inventory.Add(null);
        }
        inventoryFull = false;
    }

    public void InsertItem(GameObject go, int index)
    {
        if (go != null && !inventoryFull)
        {
            //Add item to player inventory and disable/change parent for GameObject
            inventory[index] = go;
            go.transform.parent = inventoryPanel.transform.GetChild(index).GetChild(0).transform;
            inventoryPanel.gameObject.GetComponent<InventoryPanelController>().ChangeImage(go.GetComponent<ItemSettings>().getSprite(), index);
            itemCount++;
            go.SetActive(false);

            //Disable message panel
            canvasCtrl.getMessagePanel().gameObject.SetActive(false);            

            if (itemCount == inventorySize)
            {
                inventoryFull = true;
            }

            RefreshCountPanels();
        }
    }

    public bool AddItem(GameObject go)
    {
        if (!inventoryFull)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                if (inventory[i] == null)
                {
                    InsertItem(go, i);
                    return true;
                }
            }
        }
        return false;
    }

    public void ChangeItem(GameObject go, int index)
    {
        if (go != null)
        {
            inventory[index] = go;
        }
        RefreshCountPanels();
    }

    public void SwapItems(int index1, int index2)
    {
        if (!isSlotEmpty(index1) && !isSlotEmpty(index2))
        {
            GameObject temp = inventory[index1];
            inventory[index1] = inventory[index2];
            inventory[index2] = temp;
        }
    }

    public void MoveItem(int from, int to)
    {
        if (!isSlotEmpty(from))
        {
            inventory[to] = inventory[from];
            inventory[from] = null;
        }
    }

    public void DropItem(int index)
    {
        if (!isSlotEmpty(index))
        {
            GameObject go = inventory[index];
            go.transform.parent = transform.root.parent;
            go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            go.SetActive(true);
            Remove(index);
            RefreshCountPanels();
            manager.getAudioManager().Play("Drop");
        }
    }

    public void RemoveItem(int index)
    {
        GameObject go = inventory[index];
        inventory[index] = null;
        itemCount--;
        inventoryFull = false;
        inventoryPanel.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
        Destroy(go);
        RefreshCountPanels();
    }

    public void Remove(int index)
    {
        inventory[index] = null;
        itemCount--;
        inventoryFull = false;
    }

    public void DestroyItem(int index)
    {
        if (!isSlotEmpty(index))
        {
            GameObject go = inventory[index];
            inventory[index] = null;
            inventoryPanel.gameObject.GetComponent<InventoryPanelController>().ChangeImageActiveState(false, index);
            Destroy(go);
        }
    }

    public int FindItemIndex(GameObject item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i] == item)
            {
                return i;
            }
        }

        return -1;
    }

    public bool isSlotEmpty(int index)
    {
        return inventory[index] == null;
    }

    public bool getInventoryFull()
    { return inventoryFull; }

    public void setInventoruFull(bool inventoryFull)
    { this.inventoryFull = inventoryFull; }

    public int getItemCount()
    { return itemCount; }

    public void setItemCount(int itemCount)
    { this.itemCount = itemCount; }

    public bool ContainsItem(int id)
    {
        foreach(GameObject item in inventory)
        {
            if(item != null && item.GetComponent<ItemSettings>() != null && item.GetComponent<ItemSettings>().getItemID() == id)
            {
                return true;
            }
        }
        return false;
    }

    public int ContainsItemID(string name)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].GetComponent<ItemSettings>().getName() == name)
            {
                return i;
            }
        }
        return -1;
    }

    public GameObject ContainsTameItem()
    {
        foreach (var item in inventory)
        {
            if (item != null && item.GetComponent<ItemSettings>() != null && item.tag.Equals("item"))
            {
                return item;
            }
        }
        return null;
    }

    public bool ConsumeItem(int id)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (!isSlotEmpty(i))
            {
                if (inventory[i].GetComponent<ItemSettings>().getItemID().Equals(id))
                {
                    RemoveItem(i);
                    return true;
                }
            }
        }
        return false;
    }
    public bool RemoveItem(string name)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (!isSlotEmpty(i))
            {
                if (inventory[i].GetComponent<ItemSettings>().getName() == name)
                {
                    RemoveItem(i);
                    return true;
                }
            }            
        }
        return false;
    }

    public bool isFull()
    {
        return inventoryFull;
    }

    public int ItemCount(string name)
    {
        int count = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (!isSlotEmpty(i))
            {
                if (inventory[i].GetComponent<ItemSettings>().getName() == name)
                {
                    count++;
                }
            }
        }
        return count;        
    }

    public void RefreshCountPanels()
    {       
        craftingPanel.transform.GetChild(0).GetChild(0).GetComponent<RecipeController>().RefreshRecipeAvailability();
        craftingPanel.transform.GetChild(1).GetComponent<CraftItemController>().FormatCountText();

        if(shipPartPanel.transform.GetChild(0).childCount >= 0)
        {
            foreach (Transform child in shipPartPanel.transform.GetChild(0))
            {
                child.GetComponent<ShipPartSlotController>().RefreshSlotCount();
            }
        }        
    }

    public List<GameObject> getInventory()
    { return inventory; }
}
