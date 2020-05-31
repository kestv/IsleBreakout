using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DependencyManager : MonoBehaviour
{
    [Header("UI Refrences")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject itemInfoCanvas;

    [Header("Player refrences")]
    [SerializeField] private int inventorySize;
    [SerializeField] private GameObject player;

    [Header("Renderers")]
    [SerializeField] private GameObject canvasPlayerRenderer;
    [SerializeField] private GameObject canvasShipRenderer;

    [Header("Other refrences")]
    [SerializeField] private ShipBuilder shipBuilder;

    //Children of UI_Canvas
    private GameObject inventoryPanel;
    private GameObject messagePanel;
    private GameObject craftingPanel;
    private GameObject equipPanel;
    private GameObject resourceGatherPanel;
    private GameObject shipRepairPanel;
    private CanvasController canvasCtrl;

    //Armor equippers
    private ArmorEquipper playerModel;
    private ArmorEquipper playerUIModel;
    private EquipSlotPanelController equipSlotPanelController;

    private AudioManager audioManager;

    private void Awake()
    {
        canvasCtrl          = canvas.GetComponent<CanvasController>();
        inventoryPanel      = canvasCtrl.getInventoryPanel().gameObject;
        messagePanel        = canvasCtrl.getMessagePanel().gameObject;
        craftingPanel       = canvasCtrl.getCraftingPanel().gameObject;
        equipPanel          = canvasCtrl.getEquipPanel().gameObject;
        resourceGatherPanel = canvasCtrl.getResourceGatherImage().gameObject;
        shipRepairPanel     = canvasCtrl.getShipRepairPanel().gameObject;        
        
        equipSlotPanelController = equipPanel.transform.GetChild(0).GetChild(2).GetComponent<EquipSlotPanelController>();

        canvasPlayerRenderer = Instantiate(canvasPlayerRenderer);
        canvasShipRenderer = Instantiate(canvasShipRenderer);

        playerModel = player.GetComponent<ArmorEquipper>();
        playerUIModel = canvasPlayerRenderer.transform.GetChild(0).GetChild(0).GetComponent<ArmorEquipper>();        

        itemInfoCanvas = Instantiate(itemInfoCanvas);

        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        StartCoroutine(Wait(100));
    }

    /// <summary>
    /// Delays the call of methods inside this coroutine
    /// </summary>
    /// <param name="time">How long should the coroutine wait, value of int 100 waits 1 frame</param>
    IEnumerator Wait(int time)
    {
        yield return time;    //Wait one frame

        craftingPanel.SetActive(false);
        craftingPanel.GetComponent<Image>().enabled = true;
        shipRepairPanel.SetActive(false);
        shipRepairPanel.GetComponent<Image>().enabled = true;
        resourceGatherPanel.SetActive(false);
        equipPanel.gameObject.SetActive(false);
        equipPanel.GetComponent<Image>().enabled = true;
    }

    public GameObject getCanvas()
    { return canvas; }

    public GameObject getItemInfoCanvas()
    { return itemInfoCanvas; }

    public int getInventorySize()
    { return inventorySize; }

    public GameObject getPlayer()
    { return player; }

    public GameObject getCanvasPlayerRenderer()
    { return canvasPlayerRenderer; }

    public GameObject getCanvasShipRenderer()
    { return canvasShipRenderer; }

    public CanvasController getCanvasController()
    { return canvasCtrl; }

    public ShipBuilder getShipBuilder()
    { return shipBuilder; }

    public GameObject getInventoryPanel()
    { return inventoryPanel; }

    public GameObject getMessagePanel()
    { return messagePanel; }

    public GameObject getCraftingPanel()
    { return craftingPanel; }

    public GameObject getEquipPanel()
    { return equipPanel; }

    public GameObject getResourceGatherPanel()
    { return resourceGatherPanel; }

    public GameObject getShipRepairPanel()
    { return shipRepairPanel; }

    public ArmorEquipper getPlayerEquipper()
    { return playerModel; }

    public ArmorEquipper getUIEquipper()
    { return playerUIModel; }

    public EquipSlotPanelController getequipSlotPanelController()
    { return equipSlotPanelController; }

    public AudioManager getAudioManager()
    { return audioManager; }
}
