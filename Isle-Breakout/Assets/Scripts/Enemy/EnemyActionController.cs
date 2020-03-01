using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionController : MonoBehaviour
{
    GameObject[] players = new GameObject[2];
    float distance;
    //Actions for animations
    int action;
    const int IS_IDLING = 1;
    const int IS_RUNNING = 2;
    const int IS_ATTACKING = 3;
    //Position where enemy spot is
    Vector3 spawnPos;
    //Trigger if player was found
    bool playerSpotted;
    //Targets which player to follow
    GameObject target;
    //Attack cooldown
    float lastAttack = 0;
    //Damage that enemy does
    public float damage;
    //Attack rate
    float attackRate = 2.0f;
    void Start()
    {
        playerSpotted = false;
        spawnPos = transform.position;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    
    void Update()
    {
        //Object can't handle object movement and animator setting. No idea wtf

        //switch (action)
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
        //    default: 
        //        isIdling(true);
        //        break;
        //}

        if (!playerSpotted)
        {
            foreach (var player in players)
            {
                distance = getDistance(player);
                if (distance < 10)
                {
                    playerSpotted = true;
                    target = player;
                }
                else playerSpotted = false;
            }
        }

        if (playerSpotted && !target.GetComponent<PlayerHealthController>().isDead())
        {
            followPlayer(target);
            if (getDistance(target) > 10)
            {
                playerSpotted = false;
            }
        }
        else
        {
            if (transform.position != spawnPos)
            {
                playerSpotted = false;
                goBackToCamp();
            }
            else if (transform.position == spawnPos)
            {
                playerSpotted = false;
                action = IS_IDLING;
            }
        }
    }

    float getDistance(GameObject target)
    {
        return (Math.Abs(target.transform.position.x - transform.position.x) + Math.Abs(target.transform.position.z - transform.position.z)); ;
    }

    void followPlayer(GameObject player)
    {
        if (getDistance(player) > 1.5f)
        {
            action = IS_RUNNING;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5 * Time.deltaTime);
            //This is for cooldown before attacking if target met;
            lastAttack = Time.time - 1.5f;
        }
        else 
        {
            action = IS_ATTACKING;
            attack(target);
        }
    }

    

    void attack(GameObject target)
    {
        if(lastAttack + attackRate < Time.time)
        {
            lastAttack = Time.time;
            target.GetComponent<PlayerHealthController>().doDamage(damage);
        }
       
    }

    void goBackToCamp()
    {
        action = IS_RUNNING;
        transform.position = Vector3.MoveTowards(transform.position, spawnPos, 5 * Time.deltaTime);
    }

    void isRunning(bool isRunning)
    {
        if(transform.GetComponent<Animator>().GetBool("isRunning") != true)
            transform.GetComponent<Animator>().SetBool("isRunning", isRunning);
    }

    void isIdling(bool isIdling)
    {
        if(transform.GetComponent<Animator>().GetBool("isIdling") != true)
            transform.GetComponent<Animator>().SetBool("isIdling", isIdling);
    }

    void isAttacking(bool isAttacking)
    {
        if (transform.GetComponent<Animator>().GetBool("isAttacking") != true)
            transform.GetComponent<Animator>().SetBool("isAttacking", isAttacking);
    }
}
