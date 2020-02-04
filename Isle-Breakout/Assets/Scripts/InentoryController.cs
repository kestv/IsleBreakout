using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InentoryController : MonoBehaviour
{
    private bool inventoryEnabled;
    public GameObject inventory;
    private int slotCount;              //Total number of slots in inventory
    private int availableSlotCount;     //Number of slots available (Higher tier bagpacks enable more slots)
    private GameObject[] slot;
    public GameObject slotHolder;

    void Start()
    {
        slotCount = 24;
        slot = new GameObject[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //Switches inventoryEnabled status every time a set button is pressed
            inventoryEnabled = !inventoryEnabled;
            Debug.Log("I has been pressed");
        }

        if (inventoryEnabled)
        {
            inventory.SetActive(true);
            Debug.Log("SetActive = true;");
        }
        else
        {
            inventory.SetActive(false);
            Debug.Log("SetActive = false;");
        }
    }
}
