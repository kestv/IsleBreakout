using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    GameObject[] players = new GameObject[2];
    float distance;
    const int IS_IDLING = 1;
    const int IS_RUNNING = 2;
    const int IS_ATTACKING = 3;
    int action;
    bool isFollowing;
    Vector3 spawnPos;
    void Start()
    {
        spawnPos = transform.position;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    
    void Update()
    {
        //Object can't handle object movement and animator setting. No idea wtf

        //switch(action)
        //{
        //    case 1:
        //        isIdling(true);
        //        break;
        //    case 2:
        //        isRunning(true);
        //        break;
        //    case 3:
        //        isAttacking(true);
        //        break;
        //}

        foreach (var player in players)
        {
            distance = (Math.Abs(player.transform.position.x - transform.position.x) + Math.Abs(player.transform.position.z - transform.position.z));
            //TODO: if(isFollowing), coz it will jump between players, must fix player when following has started.
            if (distance < 10)
            {
                isFollowing = true;
                action = IS_RUNNING;
                followPlayer(player);
                //Debug.Log("found player at coordinates: " + player.transform.position + ", distance is: " + distance);
            }
            else if(transform.position != spawnPos)
            {
                action = IS_RUNNING;
                goBackToCamp();
            }
            else if(transform.position == spawnPos)
            {
                isFollowing = false;
                action = IS_IDLING;
            }
        }
    }

    void followPlayer(GameObject player)
    {
        if (distance > 1)
        {
            action = IS_IDLING;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
        }
    }

    void goBackToCamp()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnPos, 5 * Time.deltaTime);
    }

    void isRunning(bool isMoving)
    {
        transform.GetComponent<Animator>().SetBool("isRunning", isMoving);
    }

    void isIdling(bool isIdling)
    {
        transform.GetComponent<Animator>().SetBool("isMoving", isIdling);
    }

    void isAttacking(bool isAttacking)
    {
        transform.GetComponent<Animator>().SetBool("isAttacking", isAttacking);
    }
}
