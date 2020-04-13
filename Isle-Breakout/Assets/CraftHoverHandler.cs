﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CraftingRecipe recipe;
    public GameObject item;
    public GameObject infoPanelPrefab;
    public ItemInfoPanelController panelCtrl;
    public GameObject infoPanel;
    public int moveRight;
    public int moveUp;

    public void OnPointerEnter(PointerEventData eventData)
    {
        recipe = transform.parent.GetComponent<CraftItemController>().getRecipe();        
        InitPanel(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TerminatePanel();
    }

    public void InitPanel(PointerEventData eventData)
    {
        item = recipe.getCraftedItem();

        if (item != null)
        {
            infoPanel = Instantiate(infoPanelPrefab, transform.root);
            panelCtrl = infoPanel.GetComponent<ItemInfoPanelController>();
            panelCtrl.InitPanel(item.GetComponent<ItemSettings>());
        }

        panelCtrl.transform.position = new Vector2(eventData.position.x + moveRight, eventData.position.y + moveUp);
        Destroy(infoPanel, 1.5f);
    }

    public void TerminatePanel()
    {
        if (infoPanel != null)
        {
            Destroy(infoPanel);
        }
    }
}
