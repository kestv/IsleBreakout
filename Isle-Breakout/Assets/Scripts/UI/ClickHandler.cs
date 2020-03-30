using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public PlayerInventory inventory;   //Get from constants
    public GameObject itemBeingClicked;

    private void Start()
    {
        inventory = GameObject.Find("PlayerInstance").GetComponent<PlayerInventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        itemBeingClicked = gameObject;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //UseItem() implementation
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            //Not needed?
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventory.DropItem(itemBeingClicked.transform.parent.GetComponent<SlotController>().getSlotIndex());
        }
    }
}
