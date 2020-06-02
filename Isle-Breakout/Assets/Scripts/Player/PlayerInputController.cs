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
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            GetComponent<PlayerMovementController>().enabled = false;
            transform.position = new Vector3(457, 50, -259);
            StartCoroutine(Wait());

        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            GetComponent<PlayerMovementController>().enabled = false;
            transform.position = new Vector3(-173, 50, -39);
            StartCoroutine(Wait());
            
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            GetComponent<PlayerMovementController>().enabled = false;
            transform.position = new Vector3(235, 50, 304);
            StartCoroutine(Wait());

        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            GetComponent<PlayerMovementController>().enabled = false;
            transform.position = new Vector3(-203, 50, 281);
            StartCoroutine(Wait());

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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<PlayerMovementController>().enabled = true;
    }
}
