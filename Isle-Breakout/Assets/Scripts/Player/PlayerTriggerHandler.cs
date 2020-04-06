using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTriggerHandler : MonoBehaviour
{
    public DependencyManager manager;
    public GameObject player;
    public PlayerInventory inventory;
    public GameObject canvas;
    public GameObject messagePanel;
    public GameObject messagePanelText;
    public GameObject item;
    public GameObject trigger;

    public GameObject shipCanvas;
    public ShipPartController shipPartCtrl;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = gameObject.GetComponent<PlayerInventory>();
        canvas = manager.getCanvas();
        messagePanel = canvas.transform.Find("UI_MessagePanel").gameObject;
        messagePanelText = messagePanel.transform.GetChild(0).gameObject;
        item = null;

        shipCanvas = manager.getShipCrafting();
        shipPartCtrl = shipCanvas.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<ShipPartController>();
        player = manager.getPlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (trigger)
            {
                switch (trigger.tag)
                {
                    case "item":
                        PickUpItem();
                        break;
                    case "chest":
                        trigger.GetComponent<ChestSettings>().getChestPanel().SetActive(true);
                        messagePanel.SetActive(false);
                        break;
                    case "craft":
                        shipCanvas.SetActive(true);
                        shipPartCtrl.RefreshRecipes();
                        messagePanel.SetActive(false);
                        player.GetComponent<PlayerMovementController>().enabled = false;
                        ChangeMainCanvasState(false);
                        break;
                    default:
                        break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            shipCanvas.SetActive(false);
            player.GetComponent<PlayerMovementController>().enabled = true;
            ChangeMainCanvasState(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            trigger = other.gameObject;
            string itemName = other.GetComponent<ItemSettings>().getName();
            if (itemName != "")
            {
                messagePanelText.GetComponent<TextMeshProUGUI>().text = "Pick up <#ffffff>" + itemName + "</color> with <#ffffff>'F'</color>";
            }
            else
            {
                messagePanelText.GetComponent<TextMeshProUGUI>().text = "Pick up " + "item" + " with 'F'";
            }
            messagePanel.SetActive(true);
        }
        if(other.tag == "chest")
        {
            trigger = other.gameObject;
            messagePanelText.GetComponent<TextMeshProUGUI>().text = "Open chest with <#ffffff>'F'</color>";
            messagePanel.SetActive(true);
        }
        if(other.tag == "craft")
        {
            trigger = other.gameObject;
            messagePanelText.GetComponent<TextMeshProUGUI>().text = "Open ship repair window with <#ffffff>'F'</color>";
            messagePanel.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "item")
        {            
            trigger = null;
        }
        if (other.tag == "chest")
        {
            other.GetComponent<ChestSettings>().getChestPanel().SetActive(false);
        }
        if (other.tag == "craft")
        {
            trigger = null;
            shipCanvas.SetActive(false);
            player.GetComponent<PlayerMovementController>().enabled = true;
        }
        messagePanel.SetActive(false);
        trigger = null;
    }

    public void PickUpItem()
    {
        GameObject go = trigger;
        trigger = null;
        if (!inventory.AddItem(go))
        {
            trigger = go;
        }        
    }

    public void ChangeMainCanvasState(bool state)
    {
        canvas.GetComponent<Canvas>().enabled = state;
    }
}
