using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTriggerHandler : MonoBehaviour
{
    public DependencyManager manager;
    public PlayerInventory inventory;
    public GameObject canvas;
    public GameObject messagePanel;
    public GameObject messagePanelText;
    public GameObject item;
    public GameObject trigger;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = gameObject.GetComponent<PlayerInventory>();
        canvas = manager.getCanvas();
        messagePanel = canvas.transform.Find("UI_MessagePanel").gameObject;
        messagePanelText = messagePanel.transform.GetChild(0).gameObject;
        item = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            inventory.AddItem(item);
            item = null;

            if (trigger != null && trigger.tag == "chest")
            {
                trigger.GetComponent<ChestSettings>().getChestPanel().SetActive(true);
                messagePanel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            item = other.gameObject;
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

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "item")
        {            
            item = null;
        }
        if (other.tag == "chest")
        {
            other.GetComponent<ChestSettings>().getChestPanel().SetActive(false);
        }
        messagePanel.SetActive(false);
        trigger = null;
    }
}
