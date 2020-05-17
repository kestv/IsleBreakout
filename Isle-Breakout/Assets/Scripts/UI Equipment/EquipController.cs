using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipController : MonoBehaviour
{
    private DependencyManager manager;

    private GameObject playerModel;
    private GameObject equipSlotSpritePanel;
    private GameObject equipSlotPanel;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();

        playerModel = transform.GetChild(0).GetChild(0).gameObject;
        equipSlotSpritePanel = transform.GetChild(0).GetChild(1).gameObject;
        equipSlotPanel = transform.GetChild(0).GetChild(2).gameObject;

        playerModel.GetComponent<PanelObjectRotator>().setObject(manager.getCanvasPlayerRenderer().transform.GetChild(0).GetChild(0).gameObject);
    }


}
