using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviourPun
{
    public float offY;
    public float offZ;
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
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y + offY, target.transform.position.z - offZ);
        transform.position = targetPos;
    }


}
