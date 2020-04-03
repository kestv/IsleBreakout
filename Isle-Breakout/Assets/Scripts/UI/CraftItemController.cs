using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftItemController : MonoBehaviour
{
    public DependencyManager manager;
    public PlayerInventory inventory;
    public CraftingRecipe recipe;
    public GameObject sprite;
    public GameObject text;
    public GameObject slotPanel;
    public GameObject slotPrefab;
    public List<GameObject> slots;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();

        sprite = transform.GetChild(0).GetChild(0).gameObject;
        text = transform.GetChild(1).GetChild(0).gameObject;
        slotPanel = transform.GetChild(2).gameObject;
    }

    public void InitItem(CraftingRecipe recipe, Sprite sprite, string text)
    {
        //inventory = manager.getPlayer().GetComponent<PlayerInventory>();
        this.recipe = recipe;

        foreach (Transform child in slotPanel.transform)
        {
            Destroy(child.gameObject);
        }

        setSprite(sprite);
        setText(text);

        foreach(Item item in recipe.getMaterials())
        {
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(slotPanel.transform);
            CraftItemSlotController slotController = slot.GetComponent<CraftItemSlotController>();

            ItemSettings itemSettings = item.requiredItem.GetComponent<ItemSettings>();
            slotController.setSprite(itemSettings.getSprite());
            slotController.setText(itemSettings.getName());
            slotController.setCountText(string.Format("{0}/{1}", inventory.ItemCount(itemSettings.getName()), item.count));
        }
    }

    public void Craft()
    {
        GameObject craftedItem = recipe.Craft(inventory);
        if (craftedItem)
        {
            inventory.AddItem(craftedItem);
        }
    }

    public Sprite getSprite()
    { return sprite.GetComponent<Image>().sprite; }

    public void setSprite(Sprite sprite)
    { this.sprite.GetComponent<Image>().sprite = sprite; }

    public string getText()
    { return text.GetComponent<TextMeshProUGUI>().text; }

    public void setText(string text)
    { this.text.GetComponent<TextMeshProUGUI>().text = text; }

}
