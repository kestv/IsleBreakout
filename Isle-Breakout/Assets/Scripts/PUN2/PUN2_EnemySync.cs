using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUN2_EnemySync : MonoBehaviourPun, IPunObservable
{
    Vector3 latestPos;
    Quaternion latestRot;
    GameObject latestTarget;
    bool latestPlayerSpotted;
    EnemyActionController action;
    public MonoBehaviour[] localScripts;
    public GameObject[] localObjects;

    public bool lastTrigger;

    public void Start()
    {
        latestPlayerSpotted = false;
        lastTrigger = !latestPlayerSpotted;

        action = transform.GetComponent<EnemyActionController>();
        if (photonView.IsMine)
        {
            //Player is local
        }
        else
        {
            //Player is Remote, deactivate the scripts and object that should only be enabled for the local player
            for (int i = 0; i < localScripts.Length; i++)
            {
                localScripts[i].enabled = false;
            }
            for (int i = 0; i < localObjects.Length; i++)
            {
                localObjects[i].SetActive(false);
            }
        }

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime * 5);
        }
    }
}
