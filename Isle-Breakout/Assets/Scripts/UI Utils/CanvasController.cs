using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private Transform messagePanel;
    [SerializeField] private Transform craftingPanel;
    [SerializeField] private Transform equipPanel;
    [SerializeField] private Transform resourceGatherImage;
    [SerializeField] private Transform shipRepairPanel;

    public void DisableAllPanelsExcept(Transform panel)
    {
        for (int i = 2; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) != panel)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public Transform getInventoryPanel()
    { return inventoryPanel; }

    public Transform getMessagePanel()
    { return messagePanel; }

    public Transform getCraftingPanel()
    { return craftingPanel; }

    public Transform getEquipPanel()
    { return equipPanel; }

    public Transform getResourceGatherImage()
    { return resourceGatherImage; }

    public Transform getShipRepairPanel()
    { return shipRepairPanel; }

}
