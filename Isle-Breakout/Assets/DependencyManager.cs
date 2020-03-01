using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DependencyManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject hotbar;
    public HotbarController hotbarCtrl;

    void Start()
    {
        try
        {
            canvas = GameObject.Find("UI_LocalCanvas").GetComponent<Canvas>();
            hotbar = canvas.transform.Find("UI_IventoryHotbar").gameObject;
            hotbarCtrl = hotbar.GetComponent<HotbarController>();
        }
        catch (Exception e)
        {
            Debug.Log(this.GetType().Name + " " + e.ToString());
        }

        InitializeDependencies();
    }

    void Update()
    {
    }

    public Canvas getCanvas()
    { return canvas; }

    public void setCanvas(Canvas canvas)
    { this.canvas = canvas; }

    public GameObject getHotbar()
    { return hotbar; }

    public void setHotbar(GameObject hotbar)
    { this.hotbar = hotbar; }



    private void InitializeDependencies()
    {
        hotbarCtrl.setPlayer(this.transform.gameObject);
        hotbarCtrl.setInventory(this.transform.gameObject.GetComponent<PlayerInventory>());
    }
}
