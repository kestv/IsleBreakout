using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSettings : MonoBehaviour
{
    public GameObject chestPanelPrefab;
    public GameObject chestPanel;
    public int chestSize;

    public List<GameObject> items;

    private void Start()
    {
        chestPanel = Instantiate(chestPanelPrefab);
        chestPanel.GetComponent<ChestPanelController>().InitializeChest(chestSize);
        chestPanel.SetActive(false);

        AddItemToChest(items); ;
    }

    public void AddItemToChest(List<GameObject> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i < chestSize)
            {
                Transform slotImage = chestPanel.transform.GetChild(i).GetChild(0);
                ItemSettings itemSettings = items[i].GetComponent<ItemSettings>();

                Instantiate(items[i], slotImage);
                slotImage.GetComponent<Image>().sprite = itemSettings.getSprite();
                slotImage.gameObject.SetActive(true);
            }
        }
    }

    public GameObject getChestPanel()
    { return chestPanel; }

    public void setChestPanel(GameObject chestPanel)
    { this.chestPanel = chestPanel; }

    public int getChestSize()
    { return chestSize; }

    public void setChestSize(int chestSize)
    { this.chestSize = chestSize; }
}