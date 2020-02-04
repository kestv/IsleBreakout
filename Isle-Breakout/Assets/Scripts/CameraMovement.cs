using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviourPun
{
    public GameObject target;
    public float distance;
    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = new Quaternion(80, 0, 0, 180);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y + 20, target.transform.position.z - 15);
        transform.position = targetPos;
    }


}
