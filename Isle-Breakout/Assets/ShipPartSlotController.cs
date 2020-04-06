using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipPartSlotController : MonoBehaviour
{
    public DependencyManager manager;
    public PlayerInventory inventory;

    public GameObject imageSlot;
    public GameObject nameSlot;
    public GameObject countSlot;

    public Item item;
    public Sprite sprite;
    public string slotName;
    public string count;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();

        imageSlot = transform.GetChild(0).gameObject;
        nameSlot = transform.GetChild(1).GetChild(0).gameObject;
        countSlot = transform.GetChild(1).GetChild(1).gameObject;
        InitSlot();
    }

    public void InitSlot()
    {
        ItemSettings settings = item.requiredItem.GetComponent<ItemSettings>();
        setSprite(settings.getSprite());
        setName(settings.getName());
        setCount(item.count.ToString());
    }

    public void RefreshSlotCount()
    {
        setCount(item.count.ToString());
    }

    public void setSprite(Sprite sprite)
    {
        imageSlot.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
    }

    public void setName(string name)
    {
        nameSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
    }

    public void setCount(string count)
    {
        int itemCount = inventory.ItemCount(item.requiredItem.GetComponent<ItemSettings>().getName());
        countSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemCount + "/" + count;
    }

    public void setItem(Item item)
    { this.item = item; }
}
