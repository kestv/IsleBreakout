using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private DependencyManager manager;
    private GameObject canvas;
    private CanvasController canvasController;
    private GameObject craftingPanel;
    private RecipeController recipeCtrl;
    private GameObject equipPanel;

    [Header("Items")]
    [SerializeField] List<GameObject> items;

    private List<GameObject> gameObjects;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        canvas = manager.getCanvas();
        canvasController = manager.getCanvasController();
        craftingPanel = canvasController.getCraftingPanel().gameObject;
        equipPanel = canvasController.getEquipPanel().gameObject;
        recipeCtrl = craftingPanel.transform.GetChild(0).GetChild(0).GetComponent<RecipeController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            bool isActive = craftingPanel.activeSelf;
            if (isActive)
            {
                recipeCtrl.Close();
            }
            else
            {
                canvasController.DisableAllPanelsExcept(canvasController.getCraftingPanel());
                recipeCtrl.Open();
                
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = equipPanel.activeSelf;
            if (isActive)
            {
                equipPanel.SetActive(false);
            }
            else
            {
                canvasController.DisableAllPanelsExcept(canvasController.getEquipPanel());
                equipPanel.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            //GameObject obj1 = Instantiate(items[0]);
            //GameObject obj2 = Instantiate(items[1]);
            //GameObject obj3 = Instantiate(items[2]);
            //GameObject obj4 = Instantiate(items[3]);
            //GameObject obj5 = Instantiate(items[4]);
            //GameObject obj6 = Instantiate(items[5]);
            //gameObjects.Add(obj1);
            //gameObjects.Add(obj2);
            //gameObjects.Add(obj3);
            //gameObjects.Add(obj4);
            //gameObjects.Add(obj5);
            //gameObjects.Add(obj6);

            //for (int i = 0; i < manager.getInventorySize(); i++)
            //{
            //    GetComponent<PlayerInventory>().RemoveItem(i);
            //}
            //for (int i = 0; i < manager.getInventorySize(); i++)
            //{
            //    GetComponent<PlayerInventory>().AddItem(gameObjects[i]);
            //}
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            GameObject.Find("DayLight").GetComponent<DayNight>().setSpeed(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            GameObject.Find("DayLight").GetComponent<DayNight>().setSpeed(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            GameObject.Find("DayLight").GetComponent<DayNight>().setSpeed(20);
        }
    }
}
