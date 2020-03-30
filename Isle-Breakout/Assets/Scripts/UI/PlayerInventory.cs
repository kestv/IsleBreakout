﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public DependencyManager manager;
    public List<GameObject> inventory;
    public int inventorySize;
    public GameObject canvas;
    public GameObject inventoryPanel;
    public bool isInventoryFull;
    public int itemCount;


    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();        
        canvas = manager.getCanvas();
        inventoryPanel = canvas.transform.Find("UI_InventoryPanel").gameObject;

        inventorySize = manager.getInventorySize();
        for (int i = 0; i < inventorySize; i++)
        {
            inventory.Add(null);
        }
        isInventoryFull = false;
    }

    public void InsertItem(GameObject go, int index)
    {
        if (go != null && !isInventoryFull)
        {
            //Add item to player inventory and disable/change parent for GameObject
            inventory[index] = go;
            go.transform.parent = inventoryPanel.transform.GetChild(index).GetChild(0).transform;
            inventoryPanel.gameObject.GetComponent<InventoryPanelController>().ChangeImage(go.GetComponent<ItemSettings>().getSprite(), index);
            itemCount++;
            go.SetActive(false);

            //Disable message panel
            canvas.transform.Find("UI_MessagePanel").gameObject.SetActive(false);

            if (itemCount == inventorySize)
            {
                isInventoryFull = true;
            }
        }
    }

    public void AddItem(GameObject go, int index)
    {
        if (!isInventoryFull)
        {
            if (inventory[index] == null)
            {
                InsertItem(go, index);
            }
            else
            {
                for (int i = 0; i < inventorySize; i++)
                {
                    if (inventory[i] == null)
                    {
                        InsertItem(go, i);
                        break;
                    }
                }
            }
        }
    }

    public void ChangeItem(GameObject go, int index)
    {
        if (go != null && !isInventoryFull)
        {
            inventory[index] = go;
        }
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
            //Change parent, position and active state for the GameObject
            GameObject go = inventory[index];
            go.transform.parent = transform.root.parent;
            go.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            go.SetActive(true);

            RemoveItem(index);
        }
    }

    public void RemoveItem(int index)
    {
        if (!isSlotEmpty(index))
        {
            inventory[index] = null;
            itemCount--;
            isInventoryFull = false;
        }
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

    public void UseItem(int index)
    {
        //TODO: Implement
        //ItemFunctionCall()
        //DestroyItem()
    }

    public bool isSlotEmpty(int index)
    {
        return inventory[index] == null;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.X))
        {
            DestroyItem(0);
        }
    }
    //TODO: getters & setters
}
