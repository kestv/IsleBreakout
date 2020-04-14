using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSettings : MonoBehaviour
{
    public GameObject chestPanel;
    public int chestSize;

    private void Start()
    {
        chestPanel = Instantiate(chestPanel);
        chestPanel.GetComponent<ChestPanelController>().InitializeChest(chestSize);
        chestPanel.SetActive(false);
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
