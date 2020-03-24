﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    private bool isMessagePanelActive;
    public void ToggleMessagePanel()
    {
        isMessagePanelActive = !isMessagePanelActive;
        transform.GetChild(0).gameObject.SetActive(isMessagePanelActive);
        Debug.Log("STATE TOGGLED");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleMessagePanel();
        }
    }

    private void Start()
    {
        isMessagePanelActive = false;
    }
}
