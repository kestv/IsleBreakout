using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFloat : MonoBehaviour
{    
    [SerializeField] private float offsetUp;        //Max height that the object moves up
    [SerializeField] private float offsetDown;      //Minimum height that the object moves down
    [SerializeField] private float movementValue;   //How much should the object move each update frame
    private bool floatUp;
    private float maxHeight;
    private float maxDepth;

    private void Start()
    {
        floatUp = true;
        maxHeight = transform.position.y + offsetUp;
        maxDepth = transform.position.y - offsetDown;
    }

    private void Update()
    {
        if (floatUp)
        {
            if(transform.position.y < maxHeight)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + movementValue, transform.position.z);
            }
            else
            {
                floatUp = false;
            }
        }
        else
        {
            if (transform.position.y > maxDepth)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - movementValue, transform.position.z);
            }
            else
            {
                floatUp = true;
            }
        }
    }
}
