using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionController : MonoBehaviour
{
    float speed = 5f;
    public GameObject player;
    float distance;
    //Actions for animations
    int action;
    const int IS_IDLING = 1;
    const int IS_RUNNING = 2;
    const int IS_ATTACKING = 3;
    //Position where enemy spot is
    Vector3 spawnPos;
    Vector3 spawnPos2;
    //Trigger if player was found
    public bool playerSpotted;
    //Targets which player to follow
    public GameObject target;
    //Attack cooldown
    float lastAttack = 0;
    //Damage that enemy does
    public float damage;
    //Attack rate
    float attackRate = 2.0f;
    public float fallBackDistance = 30f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;
        playerSpotted = false;
        spawnPos = transform.position;
    }

    void Update()
    {
        switch (action)
        {
            case 1:
                isIdling(true);
                break;
            case 2:
                isRunning(true);
                break;
            case 3:
                isAttacking(true);
                break;
        }

        if (!playerSpotted)
        {
            action = IS_IDLING;
            distance = getDistance(player);
            if (distance < 10)
            {
                playerSpotted = true;
            }
            else
            {
                playerSpotted = false;
            }

        }

        if ((playerSpotted && !target.GetComponent<PlayerHealthController>().isDead()))
        {
            followPlayer(target);
            if (getDistance(target) > fallBackDistance)
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
        return (Math.Abs(target.transform.position.x - transform.position.x) + Math.Abs(target.transform.position.z - transform.position.z));
    }

    void followPlayer(GameObject player)
    {
        if (getDistance(player) > 1.5f)
        {
            action = IS_RUNNING;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            var _direction = (player.transform.position - transform.position).normalized;
            var _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * speed);
            //This is for cooldown before attacking if target met;
            lastAttack = Time.time - 1.5f;
        }
        else
        {
            action = IS_IDLING;
            attack(target);
        }
    }



    void attack(GameObject target)
    {
        if (lastAttack + attackRate < Time.time)
        {
            lastAttack = Time.time;
            action = IS_ATTACKING;
            target.GetComponent<PlayerHealthController>().doDamage(damage);
        }
        else action = IS_IDLING;
    }

    void goBackToCamp()
    {
        action = IS_RUNNING;
        transform.position = Vector3.MoveTowards(transform.position, spawnPos, speed * Time.deltaTime);
        var _direction = (spawnPos - transform.position).normalized;
        var _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * speed);
    }

    void isRunning(bool isRunning)
    {
        if (transform.GetComponent<Animator>().GetBool("isRunning") != true)
        {
            transform.GetComponent<Animator>().SetBool("isRunning", isRunning);
            transform.GetComponent<Animator>().SetBool("isIdling", !isRunning);
            transform.GetComponent<Animator>().SetBool("isAttacking", !isRunning);
        }
    }

    void isIdling(bool isIdling)
    {
        if (transform.GetComponent<Animator>().GetBool("isIdling") != true)
        {
            transform.GetComponent<Animator>().SetBool("isIdling", isIdling);
            transform.GetComponent<Animator>().SetBool("isRunning", !isIdling);
            transform.GetComponent<Animator>().SetBool("isAttacking", !isIdling);
        }
    }

    void isAttacking(bool isAttacking)
    {
        if (transform.GetComponent<Animator>().GetBool("isAttacking") != true)
        {
            transform.GetComponent<Animator>().SetBool("isAttacking", isAttacking);
            transform.GetComponent<Animator>().SetBool("isRunning", !isAttacking);
            transform.GetComponent<Animator>().SetBool("isIdling", !isAttacking);
        }
    }


}
