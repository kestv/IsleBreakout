using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomDependencyManager : MonoBehaviour
{
    public GameObject player;
    public Canvas canvas;

    void Start()
    {
        try
        {
            player = transform.GetComponent<PUN2_RoomController>().getPlayer();
            canvas = GameObject.Find("UI_LocalCanvas").GetComponent<Canvas>();
        }
        catch (Exception e)
        {
            Debug.Log(this.GetType().Name + " " + e.ToString());
        }

        canvas.GetComponent<CanvasSettings>().setTargetPlayer(player);
        
    }

    public GameObject getPlayer()
    { return player; }

    public void setPlayer(GameObject player)
    { this.player = player; }

    public Canvas getCanvas()
    { return canvas; }

    public void setCanvas(Canvas canvas)
    { this.canvas = canvas; }
}
