using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DependencyManager : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas; 
    public int inventorySize;

    public GameObject shipCrafting;
    public ShipBuilder shipBuilder;
    public GameObject canvasShipRenderer;

    public GameObject equipPanel;
    public EquipSlotPanelController equipSlotPanelController;
    public ArmorEquipper playerModel;
    public ArmorEquipper playerUIModel;
    public GameObject canvasPlayerRenderer;

    public GameObject resourceGatherImage;

    private void Awake()
    {
        player = GameObject.Find("PlayerInstance");
        canvas = Instantiate(canvas);
        canvas.transform.GetChild(2).gameObject.SetActive(false);

        equipPanel = canvas.transform.GetChild(3).gameObject;
        equipSlotPanelController = equipPanel.transform.GetChild(0).GetChild(2).GetComponent<EquipSlotPanelController>();

        resourceGatherImage = canvas.transform.GetChild(4).GetChild(0).gameObject;

        inventorySize = 6;

        shipCrafting = canvas.transform.GetChild(5).gameObject;
        shipBuilder = GameObject.Find("SHIP").GetComponent<ShipBuilder>();

        canvasPlayerRenderer = Instantiate(canvasPlayerRenderer);
        playerModel = player.GetComponent<ArmorEquipper>();
        playerUIModel = canvasPlayerRenderer.transform.GetChild(0).GetChild(0).GetComponent<ArmorEquipper>();

        canvasShipRenderer = Instantiate(canvasShipRenderer);
    }

    private void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        yield return 100;    //Wait one frame
        shipCrafting.SetActive(false);        
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        resourceGatherImage.transform.parent.gameObject.SetActive(false);
    }

    public GameObject getPlayer()
    { return player; }

    public void setPlayer(GameObject player)
    { this.player = player; }

    public GameObject getCanvas()
    { return canvas; }

    public void setCanvas(GameObject canvas)
    { this.canvas = canvas; }

    public int getInventorySize()
    { return inventorySize; }

    public void setInventorySize(int inventorySize)
    { this.inventorySize = inventorySize; }

    public GameObject getShipCrafting()
    { return shipCrafting; }

    public void setShipCrafting(GameObject shipCrafting)
    { this.shipCrafting = shipCrafting; }

    public ShipBuilder getShipBuilder()
    { return shipBuilder; }

    public void setShipBuilder(ShipBuilder shipBuilder)
    { this.shipBuilder = shipBuilder; }

    public GameObject getEquipPanel()
    { return equipPanel; }

    public EquipSlotPanelController getequipSlotPanelController()
    { return equipSlotPanelController; }

    public ArmorEquipper getPlayerEquipper()
    { return playerModel; }

    public ArmorEquipper getUIEquipper()
    { return playerUIModel; }

    public GameObject getCanvasPlayerRenderer()
    { return canvasPlayerRenderer; }

    public GameObject getResourceGatherImage()
    { return resourceGatherImage; }

    public void setResourceGatherImage(GameObject resourceGatherImage)
    { this.resourceGatherImage = resourceGatherImage; }

    public GameObject getCanvasShipRenderer()
    { return canvasShipRenderer; }
}
