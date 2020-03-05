using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectsSpawnController : MonoBehaviourPun
{
    public GameObject playerCamera;
    void Start()
    {
        if (photonView.IsMine)
        {
            playerCamera = PhotonNetwork.Instantiate(playerCamera.name, transform.position, Quaternion.identity, 0);
            playerCamera.GetComponent<Camera>().enabled = true;
            playerCamera.GetComponent<CameraMovement>().enabled = true;
            playerCamera.GetComponent<CameraMovement>().target = gameObject;
        }
    }
}
