using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollisionController : MonoBehaviourPun
{
    public GameObject canvas;

    private CanvasController script;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "item")
        {
            script.ToggleMessagePanel();
            Debug.Log("Triggered OnCollisionEnter");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "item")
        {
            script.ToggleMessagePanel();
            Debug.Log("Triggered OnCollisionExit");
        }
    }

    private void Start()
    {
        canvas = GameObject.Find("PlayerCanvas");
        //script = canvas.GetComponent(typeof(CanvasController)) as CanvasController;
        script = canvas.GetComponent<CanvasController>();
    }
}
