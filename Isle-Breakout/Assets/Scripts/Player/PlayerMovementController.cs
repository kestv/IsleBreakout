﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public GameObject cam;
    Transform camTransform;
    public bool isRunning;
    public CharacterController controller;
    public float speed = 8f;
    public Vector3 velocity;
    public float gravity = -15f;
    public Transform groundCheck;
    public LayerMask layerMask;
    bool isGrounded;
    public bool canAnimate;
    private void Start()
    {
        canAnimate = true;
        cam = GameObject.Find("Main Camera");
        isRunning = false;
        camTransform = cam.transform;
    }
    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, camTransform.eulerAngles.y, 0);
        //transform.Rotate(-30, 0, 0);
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, layerMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 rot = transform.right * x + transform.forward * z;
        Vector3 move = transform.right * x + transform.forward * z / (Mathf.Abs(x) + Mathf.Abs(z));
        if (Vector3.zero != rot)
        {
            transform.rotation = Quaternion.LookRotation(rot);
            controller.Move(move * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        if (!isGrounded)
        {
            controller.Move(velocity * Time.deltaTime);
        }

        if (x != 0 || z != 0)
        {
            isRunning = true;
            GetComponent<Animator>().SetBool("Running", true);
        }
        else
        {
            isRunning = false;
            if(canAnimate)
                GetComponent<Animator>().SetBool("Running", false);
        }
    }
}
