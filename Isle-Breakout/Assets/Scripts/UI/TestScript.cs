using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public DependencyManager manager;
    public GameObject player;
    public PlayerInventory inventory;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        player = manager.getPlayer();
        inventory = player.GetComponent<PlayerInventory>();
    }

    public void GetCountCloth()
    {
        Debug.Log(inventory.ItemCount("Cloth"));
    }
}
