﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    private DependencyManager manager;
    private PlayerInventory inventory;
    private GameObject itemBeingClicked;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        itemBeingClicked = gameObject;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject item = itemBeingClicked.transform.GetChild(0).gameObject;
            ItemConsumable consumable = item.GetComponent<ItemConsumable>();
            ScriptableObject tempEqp = item.GetComponent<ItemSettings>().getEquip();

            if(consumable != null)
            {
                consumable.Consume();

                inventory.RemoveItem(transform.parent.GetSiblingIndex());
                Destroy(item);
                gameObject.SetActive(false);
                manager.getAudioManager().Play("Eat");
            }

            if (tempEqp != null)
            {
                IArmor equip = (IArmor)tempEqp;
                manager.getAudioManager().Play("Equip");

                Transform slot = manager.getequipSlotPanelController().getSlot(equip.getType());

                if (!slot.GetChild(0).gameObject.activeSelf)
                {
                    //Move item
                    int index = inventory.FindItemIndex(itemBeingClicked.transform.GetChild(0).gameObject);

                    if (index != -1)
                    {
                        inventory.Remove(index);
                    }

                    itemBeingClicked.transform.GetChild(0).transform.SetParent(slot.GetChild(0));
                    slot.GetChild(0).GetComponent<Image>().sprite = itemBeingClicked.GetComponent<Image>().sprite;
                    slot.GetChild(0).gameObject.SetActive(true);
                    gameObject.SetActive(false);

                    //TODO EQUIP ITEM
                    manager.getPlayerEquipper().Equip(equip);
                    manager.getUIEquipper().Equip(equip);

                }
                else
                {
                    //Swap items
                    IArmor unequipItem = (IArmor)slot.GetChild(0).GetChild(0).GetComponent<ItemSettings>().getEquip();
                    if (unequipItem != null)
                    {
                        manager.getPlayerEquipper().Unequip(unequipItem);
                        manager.getUIEquipper().Unequip(unequipItem);
                    }

                    itemBeingClicked.transform.GetChild(0).transform.SetParent(slot.GetChild(0));
                    slot.GetChild(0).GetChild(0).transform.SetParent(itemBeingClicked.transform);
                    Sprite tempSprite = slot.GetChild(0).GetComponent<Image>().sprite;
                    slot.GetChild(0).GetComponent<Image>().sprite = itemBeingClicked.GetComponent<Image>().sprite;
                    itemBeingClicked.GetComponent<Image>().sprite = tempSprite;

                    inventory.ChangeItem(itemBeingClicked.transform.GetChild(0).gameObject, transform.parent.GetSiblingIndex());

                    //TODO SWAP EQP
                    manager.getPlayerEquipper().Equip(equip);
                    manager.getUIEquipper().Equip(equip);
                }
            }
            GetComponent<HoverHandler>().TerminatePanel();
        }
    }
}
