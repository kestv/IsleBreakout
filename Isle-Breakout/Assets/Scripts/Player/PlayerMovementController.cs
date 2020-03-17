using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviourPun
{
    public GameObject cam;
    Transform camTransform;
    public bool isRunning;
    public float speed = 10;
    private void Start()
    {
        isRunning = false;
        camTransform = cam.transform;
    }
    private void Update()
    {
        transform.rotation = camTransform.rotation;
        transform.Rotate(-30, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            if(Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -45, 0);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, 45, 0);
            }
            transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));
            GetComponent<Animator>().SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(0, 180, 0);
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, 45, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, -45, 0);
            }
            transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));
            GetComponent<Animator>().SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -90, 0);
            if (Input.GetKey(KeyCode.W))
            {
                transform.Rotate(0, 45, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Rotate(0, -45, 0);
            }
            transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));
            GetComponent<Animator>().SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 90, 0);
            if (Input.GetKey(KeyCode.W))
            {
                transform.Rotate(0, -45, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Rotate(0, 45, 0);
            }
            transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));
            GetComponent<Animator>().SetBool("Running", true);
        }
        else if(isRunning)
        {
            GetComponent<Animator>().SetBool("Running", true);
        }
        else GetComponent<Animator>().SetBool("Running", false);
    }
}
