using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject slotPrefab;

    private DependencyManager manager;
    private int inventorySize;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventorySize = manager.getInventorySize();

        for (int i = 0; i < inventorySize; i++)
        {
            slotPrefab = Instantiate(slotPrefab);
            slotPrefab.transform.SetParent(transform, false);
            slotPrefab.GetComponent<SlotController>().setSlotIndex(i);
        }
    }

    public void ChangeImage(Sprite sprite, int index)
    {
        transform.GetChild(index).GetChild(0).GetComponent<Image>().sprite = sprite;
        transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
    }

    public void ChangeImageActiveState(bool state, int index)
    {
        transform.GetChild(index).GetChild(0).gameObject.SetActive(state);
    }
}
