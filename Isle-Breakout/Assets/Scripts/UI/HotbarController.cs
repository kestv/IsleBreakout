using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarController : MonoBehaviour
{
    private List<GameObject> slots;     //Slot container
    public int slotSelector;           //Selected (active) slot
    public GameObject player;
    public PlayerInventory inventory;

    private Color colorUnselectedSlot;
    private Color colorSelectedSlot;

    void Start()
    {
        slots = new List<GameObject>();         //Initialize slot container

        colorUnselectedSlot = new Color32(255, 255, 255, 100);
        colorSelectedSlot = new Color32(255, 255, 255, 200);

        //Initialize components
        try
        {
            foreach (Transform child in transform)  //Add all slots to slots container
            {
                slots.Add(child.gameObject);
            }
            player = transform.parent.GetComponent<CanvasSettings>().getTargetPlayer();
            inventory = player.GetComponent<PlayerInventory>();
        }
        catch (Exception e)
        {
            Debug.Log(this.GetType().Name + " " + e.ToString());
        }

        slotSelector = 0;
        ChangeSlotColor(slotSelector, colorSelectedSlot);
    }

    void Update()
    {
        //Change active slot by keyboard or mouse scroll
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeActiveSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeActiveSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeActiveSlot(2);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (slotSelector < slots.Count - 1)
            {
                ChangeActiveSlot(slotSelector + 1);
            }
            else
            {
                ChangeActiveSlot(0);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (slotSelector != 0)
            {
                ChangeActiveSlot(slotSelector - 1);
            }
            else
            {
                ChangeActiveSlot(slots.Count - 1);
            }
        }
    }

    #region Get/Set methods
    public List<GameObject> getSlots()
    { return slots; }

    public GameObject getSlot(int index)
    { return slots[index]; }

    public void setSlot(GameObject go, int index)
    { slots[index] = go; }

    public int getSlotSelector()
    { return slotSelector; }

    public void setSlotSelector(int index)
    { slotSelector = index; }

    public GameObject getPlayer()
    { return player; }

    public void setPlayer(GameObject player)
    { this.player = player; }

    public PlayerInventory getInventory()
    { return inventory; }

    public void setInventory(PlayerInventory inventory)
    { this.inventory = inventory; }
    #endregion

    public void ChangeActiveSlot(int index)
    {
        ChangeSlotColor(slotSelector, colorUnselectedSlot);
        SelectSlot(index);
    }

    public void SelectSlot(int index)
    {
        slotSelector = index;
        ChangeSlotColor(slotSelector, colorSelectedSlot);
        inventory.setItemSelector(slotSelector);
    }

    public void ChangeSlotColor(int index, Color color)
    {
        slots[index].GetComponent<Image>().color = color;
    }
}
