using System.Collections;
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
    private void Start()
    {
        isRunning = false;
        camTransform = cam.transform;
    }
    private void Update()
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
        

        Vector3 move = transform.right * x + transform.forward * z;
        if(Vector3.zero != move)
            transform.rotation = Quaternion.LookRotation(move);
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        if(x != 0 || z != 0 || isRunning)
        {
            GetComponent<Animator>().SetBool("Running", true);
        }
        else GetComponent<Animator>().SetBool("Running", false);
    }
}
