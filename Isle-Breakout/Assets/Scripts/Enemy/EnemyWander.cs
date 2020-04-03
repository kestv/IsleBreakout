using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    const int READY = 2;
    const int WANDERING = 3;
    const int WAITING = 4;
    public bool busy;

    public Vector3 startingPosition;
    public float maxDistance = 10f;
    public float waitTime = 2f;
    public float startedWaiting;
    float startedWalking;
    public int state;
    public float speed = 1f;

    public float rotation;
    float distance;

    float lastRot = 0;
    Vector3 lastPos;
    EnemyActionController actions;
    EnemyAnimationController animations;
    void Start()
    {
        busy = true;
        lastRot = transform.rotation.y;
        startingPosition = transform.position;
        actions = GetComponent<EnemyActionController>();
        animations = GetComponent<EnemyAnimationController>();
        state = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(actions != null)
        {
            if (!actions.busy)
            {
                busy = true;
                switch (state)
                {
                    case READY:
                        CalculateRotation();
                        break;
                    case WANDERING:
                        Wander();
                        break;
                    case WAITING:
                        Wait();
                        break;
                    default:
                        break;
                }
            }
            else busy = false;
        }
        else
        {
            switch (state)
            {
                case READY:
                    CalculateRotation();
                    break;
                case WANDERING:
                    Wander();
                    break;
                case WAITING:
                    Wait();
                    break;
                default:
                    break;
            }
        }
    }

    void CalculateRotation()
    {
        distance = Vector3.Distance(transform.position, startingPosition);
        rotation = Random.Range(0, 360);
        transform.LookAt(startingPosition);
        if (Mathf.Abs(transform.eulerAngles.y - rotation) < 45 || Mathf.Abs(transform.eulerAngles.y - rotation) > 305)
        {
            transform.eulerAngles = new Vector3(0, rotation, 0);
            lastRot = rotation;
            state = WANDERING;
            animations.isWalking(true);
            startedWalking = Time.time;
        }
        Debug.Log(distance);
    }

    void Wander()
    {
        distance = Vector3.Distance(transform.position, startingPosition);
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        if (distance >= maxDistance && Time.time - startedWalking > 2f)
        {
            state = WAITING;
            startedWaiting = Time.time;
        }   
    }

    void Wait()
    {
        animations.isIdling(true);
        if (Time.time - startedWaiting > waitTime)
        {
            state = READY;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        state = WAITING;
        startedWaiting = Time.time;
    }
}
