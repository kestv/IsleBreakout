using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraMovement : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("PlayerInstance");   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();   
    }

    void Move()
    {
        var pos = player.transform.position;
        pos.y = 100;
        transform.position = pos;
    }
}
