using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShipSlotController : MonoBehaviour, IPointerClickHandler
{
    //-----------------------VARIABLES-----------------------
    public CraftingRecipe recipe;

    public GameObject namePanel;
    public GameObject descriptionPanel;

    public string nameText;
    public string descriptionText;

    public ShipPartController shipPartCtrl;
    public ShipObjectController shipObjectCtrl;

    //---------------------UNITY METHODS---------------------
    private void Start()
    {
        namePanel = transform.GetChild(0).gameObject;
        descriptionPanel = transform.GetChild(1).gameObject;

        nameText = recipe.craftedItem.GetComponent<ItemSettings>().getName();
        descriptionText = recipe.craftedItem.GetComponent<ItemSettings>().getDescription();

        setNamePanelText(nameText);
        setDescriptionPanelText(descriptionText);

        shipPartCtrl = transform.root.GetChild(0).GetChild(0).GetChild(3).GetComponent<ShipPartController>();
        shipObjectCtrl = transform.root.GetChild(0).GetChild(0).GetChild(1).GetComponent<ShipObjectController>();

        UpdateUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateUI();
    }

    //------------------------METHODS------------------------
    public void UpdateUI()
    {
        shipPartCtrl.InitSlots(recipe);
        shipObjectCtrl.InitObject(recipe.craftedItem);
    }

    //------------------------GET/SET------------------------
    public CraftingRecipe getRecipe()
    { return recipe; }

    public void setRecipe(CraftingRecipe recipe)
    { this.recipe = recipe; }

    public string getNamePanelText()
    { return namePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text; }

    public void setNamePanelText(string text)
    { namePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text; }

    public string getDescriptionPanelText()
    { return descriptionPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text; }

    public void setDescriptionPanelText(string text)
    { descriptionPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text; }
}
