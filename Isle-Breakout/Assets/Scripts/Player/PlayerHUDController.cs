using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    [SerializeField] HUDController HUD;

    private void Start()
    {
        Instantiate(HUD);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "item")
        {
            Debug.Log("OnTriggerEnter");
            HUD.OpenMessagePanel("");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HUD.CloseMessagePanel();
        Debug.Log("OnTriggerExit");
    }
}
