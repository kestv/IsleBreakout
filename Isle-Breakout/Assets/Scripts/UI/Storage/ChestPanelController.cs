using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPanelController : MonoBehaviour
{
    public DependencyManager manager;
    public GameObject slotPrefab;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        transform.SetParent(manager.getCanvas().transform);
        GameObject canvas = manager.getCanvas();
        transform.position = new Vector3(canvas.transform.position.x, canvas.transform.position.y, canvas.transform.position.z);
    }

    public void InitializeChest(int chestSize)
    {
        for (int i = 0; i < chestSize; i++)
        {
            slotPrefab = Instantiate(slotPrefab);
            slotPrefab.transform.SetParent(transform);
            slotPrefab.GetComponent<SlotController>().setSlotIndex(i);
        }
    }
}
