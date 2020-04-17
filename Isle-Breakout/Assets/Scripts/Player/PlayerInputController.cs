using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public DependencyManager manager;
    public GameObject canvas;
    public CanvasController canvasController;

    public GameObject craftingPanel;
    public RecipeController recipeCtrl;
    public GameObject equipPanel;
    
    

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        canvas = manager.getCanvas();
        canvasController = manager.getCanvasController();
        craftingPanel = canvas.transform.Find("UI_CraftingPanel").gameObject;
        equipPanel = canvas.transform.GetChild(3).gameObject;
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
    }
}
