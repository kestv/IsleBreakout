using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject item;
    public GameObject infoPanelPrefab;
    public ItemInfoPanelController panelCtrl;
    public GameObject infoPanel;
    public int moveRight;
    public int moveUp;

    public void OnPointerEnter(PointerEventData eventData)
    {
        InitPanel(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TerminatePanel();
    }

    public void InitPanel(PointerEventData eventData)
    {
        item = transform.GetChild(0).gameObject;

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
