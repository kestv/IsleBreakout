using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public bool tamed;
    public float speed;
    GameObject player;
    EnemyAnimationController animations;
    void Start()
    {
        player = GameObject.Find("PlayerInstance");
        tamed = false;
        animations = GetComponent<EnemyAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tamed)
        {
            FollowPlayer();
        }
    }

    float GetDistance()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    void FollowPlayer()
    {
        var distance = GetDistance();
        Debug.Log(distance);
        if (distance <= 6)
        {
            animations.isIdling(true);
        }
        else if (distance > 6 && distance <= 10)
        {
            speed = 3f;
            transform.LookAt(player.transform);
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            animations.isWalking(true);
        }
        else
        {
            speed = player.GetComponent<PlayerMovementController>().speed - 1;
            transform.LookAt(player.transform);
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            animations.isRunning(true);
        }
    }
}
