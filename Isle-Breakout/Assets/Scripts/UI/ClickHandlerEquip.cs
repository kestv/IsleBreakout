using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ClickHandlerEquip : MonoBehaviour, IPointerClickHandler
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
        GameObject item = gameObject.transform.GetChild(0).gameObject;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ScriptableObject tempEqp = itemBeingClicked.transform.GetChild(0).GetComponent<ItemSettings>().getEquip();
            IArmor equip = null;

            if (tempEqp != null)
            {
                equip = (IArmor)tempEqp;
            }

            if (inventory.isFull())
            {
                //Drop item
                Transform playerPosition = manager.getPlayer().transform;
                item.transform.parent = transform.root.parent;
                item.transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, playerPosition.position.z);
                item.SetActive(true);
            }
            else
            {
                //Move item
                int index = -1;
                if (inventory.AddItem(item))
                {
                    index = inventory.FindItemIndex(item);

                    if (index > -1)
                    {
                        GameObject slot = manager.getCanvas().transform.GetChild(0).GetChild(index).gameObject;
                        slot.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }

            itemBeingClicked.SetActive(false);

            if(equip != null)
            {
                manager.getPlayerEquipper().Unequip(equip);
                manager.getUIEquipper().Unequip(equip);
            }

            GetComponent<HoverHandler>().TerminatePanel();
        }
    }
}
