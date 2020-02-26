using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarController : MonoBehaviour
{
    private List<GameObject> slots; //Slot container
    public int slotSelector;        //Selected (active) slot

    private Color colorUnselectedSlot;
    private Color colorSelectedSlot;

    #region Start/Update
    void Start()
    {
        slots = new List<GameObject>();         //Initialize slot container
        foreach (Transform child in transform)  //Add all slots to slots container
        {
            slots.Add(child.gameObject);
        }

        colorUnselectedSlot  = new Color32(255, 255, 255, 100);
        colorSelectedSlot    = new Color32(255, 255, 255, 200);

        //Initialize hotbar
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
                ChangeActiveSlot(slotSelector -1);
            }
            else
            {
                ChangeActiveSlot(slots.Count - 1);
            }            
        }
    }
    #endregion methods

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
    }

    public void ChangeSlotColor(int index, Color color)
    {
        slots[index].GetComponent<Image>().color = color;
    }
}
