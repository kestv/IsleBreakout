using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotator : MonoBehaviour
{
    public float lastFramePosition;
    public float sensitivity;

    private void Start()
    {
        sensitivity = 0.25f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastFramePosition = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            var delta = Input.mousePosition.x - lastFramePosition;
            lastFramePosition = Input.mousePosition.x;

            transform.RotateAround(transform.position, transform.up, delta * sensitivity);
        }
    }
}
