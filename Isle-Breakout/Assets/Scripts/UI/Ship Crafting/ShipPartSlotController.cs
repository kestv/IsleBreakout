using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipPartSlotController : MonoBehaviour
{
    //-----------------------VARIABLES-----------------------
    public DependencyManager manager;
    public PlayerInventory inventory;

    public GameObject imageSlot;
    public GameObject nameSlot;
    public GameObject countSlot;

    public Item item;

    //---------------------UNITY METHODS---------------------
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();

        imageSlot = transform.GetChild(0).gameObject;
        nameSlot = transform.GetChild(1).GetChild(0).gameObject;
        countSlot = transform.GetChild(1).GetChild(1).gameObject;
        InitSlot();
    }

    //------------------------METHODS------------------------
    public void InitSlot()
    {
        ItemSettings settings = item.requiredItem.GetComponent<ItemSettings>();
        setImage(settings.getSprite());
        setName(settings.getName());
        setCount(item.count.ToString());
    }

    public void RefreshSlotCount()
    {
        setCount(item.count.ToString());
    }

    //------------------------GET/SET------------------------
    public Sprite getImage()
    { return imageSlot.transform.GetChild(0).GetComponent<Image>().sprite; }

    public void setImage(Sprite sprite)
    { imageSlot.transform.GetChild(0).GetComponent<Image>().sprite = sprite; }

    public string getName()
    { return nameSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text; }

    public void setName(string name)
    { nameSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name; }

    public string getCount()
    { return countSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text; }

    public void setCount(string count)
    {
        if (inventory)
        {
            int itemCount = inventory.ItemCount(item.requiredItem.GetComponent<ItemSettings>().getName());
            countSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemCount + "/" + count;
        }
    }

    public Item getItem()
    { return item; }

    public void setItem(Item item)
    { this.item = item; }
}
