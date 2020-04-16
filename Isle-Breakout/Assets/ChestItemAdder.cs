using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemAdder : MonoBehaviour
{
    public List<GameObject> items;
    public Transform chestPanel;

    public void AddItemToChest(List<GameObject> items)
    {
        foreach(GameObject item in items)
        {
            ItemSettings itemSettings = item.GetComponent<ItemSettings>();
        }
    }
}
