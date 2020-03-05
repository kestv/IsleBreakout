using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarCanvasMovement : MonoBehaviourPun, IPunObservable
{
    public GameObject player;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        stream.SendNext(transform.position);
        stream.SendNext(transform.position);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        transform.position = new Vector3(pos.x, pos.y + 3, pos.z);
    }

    public void setPlayer(GameObject obj)
    {
        player = obj;
    }
}
