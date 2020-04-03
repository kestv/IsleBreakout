using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform lookAt;
    Transform camTransform;

    private Camera cam;

    private float distance = 10f;
    float currentX = 0f;
    float currentY = 30f;
    float sensitivityX = 4f;
    float sensitivityY = 4f;
    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
        currentY = Mathf.Clamp(currentY, 1, 89);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        var rotationY = currentY;
        Quaternion rotation = Quaternion.Euler(rotationY, currentX*sensitivityX, 0);

        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);

    }


}
