using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerTriggers : MonoBehaviour
{
    public Canvas canvas;
    public GameObject itemPanel;
    public TextMeshProUGUI itemPanelText;
    public GameObject triggeredItem;
    public PlayerInventory inventory;

    void Start()
    {
        try
        {
            canvas = GameObject.Find("UI_LocalCanvas").GetComponent<Canvas>();
            itemPanel = canvas.transform.Find("UI_ItemPanel").gameObject;
            itemPanelText = itemPanel.transform.Find("UI_ItemPanelText").GetComponent<TextMeshProUGUI>();
            inventory = transform.GetComponent<PlayerInventory>();
        }
        catch (Exception e)
        {
            Debug.Log(this.GetType().Name + " " + e.ToString());
        }

        itemPanel.SetActive(false); //itemPanel disabled by default
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))    //Pick up item
        {
            if (triggeredItem != null)
            {
                inventory.PickUp(triggeredItem);
            }
        }
        //Drop item
        if (Input.GetKeyDown(KeyCode.Q))    //Drop item
        {
            inventory.Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            triggeredItem = other.gameObject;
            itemPanelText.text = FormatMessage(triggeredItem.GetComponent<ItemParameters>().getName());
            itemPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggeredItem = null;
        itemPanel.SetActive(false);
    }

    private string FormatMessage(string itemName)
    {
        return String.Format("Pick up {0} by pressing 'E'", itemName);
    }
}
