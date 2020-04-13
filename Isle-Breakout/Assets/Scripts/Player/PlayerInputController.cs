using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public DependencyManager manager;
    public GameObject canvas;

    public GameObject craftingPanel;
    public RecipeController recipeCtrl;
    public GameObject equipPanel;
    
    

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        canvas = manager.getCanvas();
        craftingPanel = canvas.transform.Find("UI_CraftingPanel").gameObject;
        equipPanel = canvas.transform.GetChild(3).gameObject;
        recipeCtrl = craftingPanel.transform.GetChild(0).GetChild(0).GetComponent<RecipeController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            bool isActive = !craftingPanel.activeSelf;
            if (isActive)
            {
                //craftingPanel.SetActive(true);
                recipeCtrl.Open();
            }
            else
            {
                recipeCtrl.Close();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = !equipPanel.activeSelf;
            if (isActive)
            {
                equipPanel.SetActive(true);
            }
            else
            {
                equipPanel.SetActive(false);
            }
        }
    }
}
