using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlotController : MonoBehaviour
{
    public DependencyManager manager;
    public PlayerInventory inventory;
    public ArmorEquipper playerInstanceModel;
    public ArmorEquipper playerUIModel;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        GameObject player = manager.getPlayer();
        inventory = player.GetComponent<PlayerInventory>();
        playerInstanceModel = player.GetComponent<ArmorEquipper>();
    }
}
