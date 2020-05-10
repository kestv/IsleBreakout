using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RecipeSlotController : MonoBehaviour, IPointerClickHandler
{
    //------------------------Variables----------------------
    private DependencyManager manager;
    private PlayerInventory inventory;

    private GameObject slotSprite;
    private GameObject slotText;
    private GameObject slotDarkPanel;

    private CraftingRecipe recipe;
    private Sprite sprite;
    private string text;

    private GameObject craftItemPanel;
    private CraftItemController craftItemCtrl;

    private bool recipeAvailable;

    //------------------------Methods------------------------
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();

        ItemSettings craftedItemSettings = recipe.getCraftedItem().GetComponent<ItemSettings>();
        sprite = craftedItemSettings.getSprite();
        text = craftedItemSettings.getName();

        slotSprite = transform.GetChild(0).GetChild(0).gameObject;
        slotText = transform.GetChild(1).GetChild(0).gameObject;
        slotDarkPanel = transform.GetChild(2).gameObject;

        craftItemPanel = transform.parent.parent.parent.GetChild(1).gameObject;
        craftItemCtrl = craftItemPanel.GetComponent<CraftItemController>();

        setSlotImage(sprite);
        setSlotText(text);

        recipeAvailable = false;
        SetRecipeAvailability();

        craftItemCtrl.InitItem(recipe, getSlotImage(), getSlotText());  //TODO CHANGE TO FIRST-AVAILABLE
    }

    public void SetRecipeAvailability()
    {
        if (recipe.CanCraft(inventory))
        {
            recipeAvailable = true;            
        }
        else
        {
            recipeAvailable = false;
        }
        slotDarkPanel.SetActive(!recipeAvailable);
    }

    //------------------------Get/Set------------------------
    public CraftingRecipe getRecipe()
    { return recipe; }

    public void setRecipe(CraftingRecipe recipe)
    { this.recipe = recipe; }

    public Sprite getSlotImage()
    { return slotSprite.GetComponent<Image>().sprite; }

    public void setSlotImage(Sprite slotImage)
    { slotSprite.GetComponent<Image>().sprite = slotImage; }

    public string getSlotText()
    { return slotText.GetComponent<TextMeshProUGUI>().text; }

    public void setSlotText(string text)
    { slotText.GetComponent<TextMeshProUGUI>().text = text; }

    public GameObject getSlotDarkPanel()
    { return slotDarkPanel; }

    public void setSlotDarkPanel(GameObject slotDarkPanel)
    { this.slotDarkPanel = slotDarkPanel; }

    public bool getRecipeAvailable()
    { return recipeAvailable; }

    public void setRecipeAvailable(bool isAvailable)
    { recipeAvailable = isAvailable; }

    //---------------Interface implementations---------------
    public void OnPointerClick(PointerEventData eventData)
    {
        craftItemCtrl.InitItem(recipe, getSlotImage(), getSlotText());
    }
}
