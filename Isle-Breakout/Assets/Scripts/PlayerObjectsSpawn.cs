using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectsSpawn : MonoBehaviour
{
    public GameObject camera;
    public GameObject healthBarCanvas;
    void Start()
    {
        camera = PhotonNetwork.Instantiate(camera.name, transform.position, Quaternion.identity, 0);
        camera.GetComponent<Camera>().enabled = true;
        camera.GetComponent<CameraMovement>().enabled = true;
        camera.GetComponent<CameraMovement>().target = gameObject;

        Vector3 canvasPos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        healthBarCanvas = PhotonNetwork.Instantiate(healthBarCanvas.name, canvasPos,new Quaternion(0,0,0,0), 0);
        healthBarCanvas.GetComponent<HealthbarCanvasMovement>().player = gameObject;
    }

}
