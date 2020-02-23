using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviourPun
{
    public GameObject camera;
    private void Start()
    {
        camera = PhotonNetwork.Instantiate(camera.name, transform.position, Quaternion.identity, 0);
        camera.GetComponent<Camera>().enabled = true;
        camera.GetComponent<CameraMovement>().enabled = true;
        camera.GetComponent<CameraMovement>().target = gameObject;        
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(0, 45, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(0, -45, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            transform.Translate(new Vector3(0, 0, Time.deltaTime * 10f));
            GetComponent<Animator>().SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(0, 135, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(0, -135, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            
            transform.Translate(new Vector3(0, 0, Time.deltaTime * 10f));
            GetComponent<Animator>().SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.Translate(new Vector3(0, 0, Time.deltaTime * 10f));
            GetComponent<Animator>().SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.Translate(new Vector3(0, 0, Time.deltaTime * 10f));
            GetComponent<Animator>().SetBool("Running", true);
        }
        else GetComponent<Animator>().SetBool("Running", false);
    }
}
