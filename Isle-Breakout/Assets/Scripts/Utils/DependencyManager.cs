using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyManager : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas; 
    public int inventorySize;

    public GameObject shipCrafting;
    public ShipBuilder shipBuilder;

    public GameObject equipPanel;
    public EquipSlotPanelController equipSlotPanelController;
    public ArmorEquipper playerModel;
    public ArmorEquipper playerUIModel;
    public GameObject canvasPlayerRenderer;

    private void Awake()
    {
        player = GameObject.Find("PlayerInstance");
        //player = Instantiate(player, new Vector3(394, 9, -408), player.transform.rotation);
        
        //GameObject.Find("Main Camera").GetComponent<CameraMovement>().lookAt = player.transform;
        //player.GetComponent<PlayerMovementController>().cam = GameObject.Find("Main Camera");
        //player.GetComponent<PlayerHealthController>().enabled = false;
        //player.GetComponent<PlayerCombatController>().enabled = false;
        //player.GetComponent<Animator>().enabled = false;
        //player.GetComponent<PlayerSkillController>().enabled = false;
        //player.GetComponent<PlayerLevelController>().enabled = false;
        //player.GetComponent<PlayerSkillController>().enabled = false;
        //player.GetComponent<TriggerController>().enabled = false;
        //player.GetComponent<PlayerStatsController>().enabled = false;
        //player.GetComponent<SpellController>().enabled = false;
        canvas = Instantiate(canvas);
        canvas.transform.GetChild(2).gameObject.SetActive(false);

        equipPanel = canvas.transform.GetChild(3).gameObject;
        equipSlotPanelController = equipPanel.transform.GetChild(0).GetChild(2).GetComponent<EquipSlotPanelController>();

        inventorySize = 6;

        shipCrafting = Instantiate(shipCrafting);
        //shipCrafting.SetActive(false);
        shipBuilder = GameObject.Find("SHIP").GetComponent<ShipBuilder>();

        canvasPlayerRenderer = Instantiate(canvasPlayerRenderer);
        playerModel = player.GetComponent<ArmorEquipper>();
        playerUIModel = canvasPlayerRenderer.transform.GetChild(0).GetChild(0).GetComponent<ArmorEquipper>();
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
}
