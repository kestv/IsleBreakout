using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour, IDropHandler
{
    public DependencyManager manager;
    public PlayerInventory inventory;
    public int slotIndex;
    public bool isChest;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragHandler.movedToSlot = true;

        //Was item moved to another slot?
        if (DragHandler.startParent != transform)
        {
            //Is the slot empty?
            if (!transform.GetChild(0).gameObject.activeSelf)
            {
                //Move item
                DragHandler.itemBeingDragged.transform.GetChild(0).transform.parent = transform.GetChild(0);
                transform.GetChild(0).GetComponent<Image>().sprite = DragHandler.itemBeingDragged.GetComponent<Image>().sprite;
                transform.GetChild(0).gameObject.SetActive(true);
                DragHandler.movedToEmptySlot = true;

                if (!DragHandler.isItemInChest)
                {
                    if (isChest)
                    {
                        inventory.Remove(DragHandler.parentSlotIndex);
                    }
                    else
                    {
                        inventory.MoveItem(DragHandler.parentSlotIndex, slotIndex);
                    }
                }
                else
                {
                    if (!isChest)
                    {
                        inventory.ChangeItem(transform.GetChild(0).GetChild(0).gameObject, slotIndex);
                    }
                }
            }
            else
            {
                //Swap items
                DragHandler.itemBeingDragged.transform.GetChild(0).transform.parent = transform.GetChild(0);
                transform.GetChild(0).GetChild(0).transform.parent = DragHandler.itemBeingDragged.transform;
                Sprite tempSprite = transform.GetChild(0).GetComponent<Image>().sprite;
                transform.GetChild(0).GetComponent<Image>().sprite = DragHandler.itemBeingDragged.GetComponent<Image>().sprite;
                DragHandler.itemBeingDragged.GetComponent<Image>().sprite = tempSprite;

                if (!DragHandler.isItemInChest)
                {
                    if (isChest)
                    {
                        inventory.ChangeItem(DragHandler.itemBeingDragged.transform.GetChild(0).gameObject, DragHandler.parentSlotIndex);
                    }
                    else
                    {
                        inventory.SwapItems(DragHandler.parentSlotIndex, slotIndex);
                    }
                }
                else
                {
                    if (!isChest)
                    {
                        inventory.ChangeItem(transform.GetChild(0).GetChild(0).gameObject, slotIndex);
                    }
                }
            }            
        }

        DragHandler.itemBeingDragged.transform.SetParent(DragHandler.startParent);
    }

    public int getSlotIndex()
    { return slotIndex; }

    public void setSlotIndex(int slotIndex)
    { this.slotIndex = slotIndex; }

    public bool getIsChest()
    { return isChest; }

    public void setIsChest(bool isChest)
    { this.isChest = isChest; }
}