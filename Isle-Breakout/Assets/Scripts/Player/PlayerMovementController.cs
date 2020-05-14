using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    GameObject cam;
    Transform camTransform;
    bool isRunning;
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float speed = 8f;
    [SerializeField]
    Vector3 velocity;
    [SerializeField]
    float gravity = -15f;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask layerMask;
    bool isGrounded;
    bool canAnimate;
    Animator animator;
    private void Start()
    {
        canAnimate = true;
        if(cam == null)
        cam = GameObject.Find("Main Camera");
        isRunning = false;
        camTransform = cam.transform;
        animator = GetComponent<Animator>();
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
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        MovePlayer(x, z);

        velocity.y += gravity * Time.deltaTime;
        if (!isGrounded)
        {
            controller.Move(velocity * Time.deltaTime);
        }

        if (x != 0 || z != 0)
        {
            isRunning = true;
            animator.SetBool("Running", true);
        }
        else
        {
            isRunning = false;
            if(canAnimate)
                animator.SetBool("Running", false);
        }
    }

    public void MovePlayer(float x, float z)
    {
        Vector3 rot = transform.right * x + transform.forward * z;
        Vector3 move = transform.right * x + transform.forward * z / (Mathf.Abs(x) + Mathf.Abs(z));
        if (Vector3.zero != rot)
        {
            transform.rotation = Quaternion.LookRotation(rot);
            controller.Move(move * speed * Time.deltaTime);
        }
    }

    public bool GetIsRunning()
    {
        return isRunning;
    }

    public void SetIsRunning(bool isRunning)
    {
        this.isRunning = isRunning;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void AddSpeed(float speed)
    {
        this.speed += speed;
    }

    public void SetCanAnimate(bool canAnimate)
    {
        this.canAnimate = canAnimate;
    }

    //TESTS PURPOSES
    public void AssignController(CharacterController ctrl)
    {
        controller = ctrl;
    }
}
