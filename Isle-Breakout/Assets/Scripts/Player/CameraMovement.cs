using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform lookAt;
    Transform camTransform;

    private Camera cam;

    private float distance = 10f;
    float currentX = 0f;
    float currentY = 30f;
    float sensitivityX = 4f;
    float sensitivityY = 4f;
    Quaternion rotation;
    void Start()
    {
        lookAt = GameObject.Find("PlayerInstance").transform;
        rotation = Quaternion.Euler(currentY, currentX * sensitivityX, 0);
        camTransform = transform;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            currentX += Input.GetAxis("Mouse X");
            currentY += Input.GetAxis("Mouse Y");
            currentY = Mathf.Clamp(currentY, 1, 89);
        }
    }

    private void LateUpdate()
    {
        
        Vector3 dir = new Vector3(0, 0, -distance);
        var rotationY = currentY;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            rotation = Quaternion.Euler(rotationY, currentX*sensitivityX, 0);
        }
        camTransform.position = lookAt.position + rotation * dir;
            camTransform.LookAt(lookAt.position);

    }


}
