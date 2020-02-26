using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarCanvasMovement : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        transform.position = new Vector3(pos.x, pos.y + 3, pos.z);
    }
}
