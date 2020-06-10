using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(0.02f, 0.04f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z);
    }
}
