using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelController : MonoBehaviour
{
    public DependencyManager manager;
    public GameObject slotPrefab;
    public int inventorySize;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventorySize = manager.getInventorySize();

        for (int i = 0; i < inventorySize; i++)
        {
            slotPrefab = Instantiate(slotPrefab);
            slotPrefab.transform.parent = transform;
            slotPrefab.GetComponent<SlotController>().setSlotIndex(i);
        }
    }

    public void ChangeImage(Sprite sprite, int index)
    {
        //TODO: loose child dependency
        transform.GetChild(index).GetChild(0).GetComponent<Image>().sprite = sprite;
        transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
    }

    public void ChangeImageActiveState(bool state, int index)
    {
        //TODO: loose child dependency
        transform.GetChild(index).GetChild(0).gameObject.SetActive(state);
    }
}
