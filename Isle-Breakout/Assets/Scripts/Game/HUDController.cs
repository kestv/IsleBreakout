using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject messagePanel;

    public void OpenMessagePanel(string text)
    {
        Debug.Log(messagePanel.name);
        //messagePanel.SetActive(true);
        //TODO: change messagePanelText
    }

    public void CloseMessagePanel()
    {
        messagePanel.SetActive(false);
    }
}
