using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public DependencyManager manager;
    public PlayerInventory inventory;
    public static GameObject itemBeingDragged;
    public static Transform startParent;
    public static int parentSlotIndex;
    public static bool movedToSlot;
    public static bool movedToEmptySlot;
    public static bool isItemInChest;
    

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();
        isItemInChest = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {       
        isItemInChest = transform.parent.GetComponent<SlotController>().getIsChest();
        movedToSlot = false;
        movedToEmptySlot = false;
        itemBeingDragged = gameObject;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        parentSlotIndex = transform.parent.GetComponent<SlotController>().getSlotIndex();
        itemBeingDragged.transform.parent = transform.root;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (movedToSlot)
        {
            if (movedToEmptySlot)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            itemBeingDragged.transform.parent = startParent;

            if (!isItemInChest)
            {
                inventory.DropItem(startParent.GetComponent<SlotController>().getSlotIndex());
            }
            else
            {
                GameObject go = transform.GetChild(0).gameObject;
                Transform playerPosition = manager.getPlayer().transform;
                go.transform.parent = transform.root.parent;
                go.transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z);
                go.SetActive(true);
            }
            
            gameObject.SetActive(false);
        }

        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

