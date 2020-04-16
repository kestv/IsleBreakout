﻿using System.Collections;
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

    public List<GameObject> triggers;

    //NPC
    public GameObject info;
    int ID = 0;
    List<string> conversations;
    List<Quest> quests;
    string npcName;

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

        triggers = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (triggers.Count > 0)
            {
                switch (triggers[triggers.Count - 1].tag)
                {
                    case "item":
                        PickUpItem();
                        break;
                    case "chest":
                        triggers[triggers.Count - 1].GetComponent<ChestSettings>().getChestPanel().SetActive(true);
                        messagePanel.SetActive(false);
                        break;
                    case "craft":
                        shipCanvas.SetActive(true);
                        shipPartCtrl.RefreshRecipes();
                        messagePanel.SetActive(false);
                        player.GetComponent<PlayerMovementController>().enabled = false;
                        ChangeMainCanvasState(false);
                        break;
                    case "resource":
                        if (!inventory.isFull())
                        {
                            triggers[triggers.Count - 1].GetComponent<ResourceGatherer>().Gather();
                            messagePanel.SetActive(false);
                        }
                        else
                        {
                            SetMessagePanelText("Inventory is full!");
                        }
                        break;
                    case "Npc":
                        NpcEventHandler.Instance.onTalkedToNpc(ID, conversations, npcName, quests);
                        if (NpcEventHandler.Instance._onTalkedToNpc != null)
                        {
                            NpcEventHandler.Instance._onTalkedToNpc();
                        }
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
            UpdateTriggerMessage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            triggers.Add(other.gameObject);
            SetMessagePanelText(other.gameObject);
        }
        if (other.tag == "chest")
        {
            triggers.Add(other.gameObject);
            SetMessagePanelText(other.gameObject);
        }
        if (other.tag == "craft")
        {
            triggers.Add(other.gameObject);
            SetMessagePanelText(other.gameObject);
        }
        if (other.tag == "resource")
        {
            triggers.Add(other.gameObject);
            SetMessagePanelText(other.gameObject);
        }
        if (other.tag == "Npc")
        {
            var npcCtrl = other.gameObject.GetComponent<NpcController>();
            if (npcCtrl != null)
            {
                triggers.Add(other.gameObject);
                ID = npcCtrl.ID;
                conversations = npcCtrl.conversations;
                npcName = npcCtrl.name;
                quests = npcCtrl.quests;
                SetMessagePanelText(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "item")
        {
            triggers.Remove(other.gameObject);
        }
        if (other.tag == "chest")
        {
            triggers.Remove(other.gameObject);
            other.GetComponent<ChestSettings>().getChestPanel().SetActive(false);
        }
        if (other.tag == "craft")
        {
            triggers.Remove(other.gameObject);
            shipCanvas.SetActive(false);
            player.GetComponent<PlayerMovementController>().enabled = true;
            ChangeMainCanvasState(true);
        }
        if (other.tag == "resource")
        {
            triggers.Remove(other.gameObject);
        }
        if (other.gameObject.tag == "Npc")
        {
            triggers.Remove(other.gameObject);
        }

        UpdateTriggerMessage();
    }

    public void SetMessagePanelText(string text)
    {
        messagePanelText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetMessagePanelText(GameObject go)
    {
        bool tagFound = true;

        switch (go.tag)
        {
            case "item":
                string itemName = go.GetComponent<ItemSettings>().getName();
                if (itemName != "")
                {
                    SetMessagePanelText("Pick up <#ffffff>" + itemName + "</color> with <#ffffff>'F'</color>");
                }
                else
                {
                    SetMessagePanelText("Pick up item with <#ffffff>'F'</color>");
                }
                break;
            case "chest":
                SetMessagePanelText("Open chest with <#ffffff>'F'</color>");
                break;
            case "craft":
                SetMessagePanelText("Open ship repair window with <#ffffff>'F'</color>");
                break;
            case "resource":
                SetMessagePanelText("Gather resource with <#ffffff>'F'</color>");
                break;
            case "Npc":
                SetMessagePanelText("Talk to " + npcName + " with <#ffffff>'F'</color>");
                break;
            default:
                tagFound = false;
                break;
        }

        if (tagFound)
        {
            messagePanel.gameObject.SetActive(true);
        }
    }

    public void UpdateTriggerMessage()
    {
        if (triggers.Count == 0)
        {
            messagePanel.SetActive(false);
        }
        else
        {
            SetMessagePanelText(triggers[triggers.Count - 1]);
        }
    }

    public void PickUpItem()
    {
        GameObject go = triggers[triggers.Count - 1];

        if (inventory.AddItem(go))
        {
            triggers.Remove(triggers[triggers.Count - 1]);
            UpdateTriggerMessage();
        }
        else
        {
            SetMessagePanelText("Inventory is full!");
        }
    }

    public void ChangeMainCanvasState(bool state)
    {
        canvas.GetComponent<Canvas>().enabled = state;
    }

    public void RemoveTrigger(GameObject go)
    {
        if (go != null && triggers.Contains(go))
        {
            triggers.Remove(go);
        }
    }
}
