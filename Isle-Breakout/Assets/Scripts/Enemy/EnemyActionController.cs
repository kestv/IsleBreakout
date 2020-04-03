﻿using System;
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
    public Vector3 spawnPos;
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

    public bool busy;

    public EnemyAnimationController animations;

    void Start()
    {
        animations = GetComponent<EnemyAnimationController>();
        busy = false;
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;
        playerSpotted = false;
        StartCoroutine(Landing());
    }

    IEnumerator Landing()
    {
        yield return new WaitForSeconds(5f);
        spawnPos = transform.position;
        if(GetComponent<EnemyWander>() != null)
            GetComponent<EnemyWander>().enabled = true;
    }

    void Update()
    {
        if (busy)
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
        }

        if (!playerSpotted)
        {
            action = IS_IDLING;
            distance = getDistance(player);
            if (distance < 10)
            {
                busy = true;
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
            if (Vector3.Distance(transform.position, spawnPos) > 0.5f)
            {
                playerSpotted = false;
                goBackToCamp();
            }
            else if (Vector3.Distance(transform.position, spawnPos) <= 0.5f)
            {
                playerSpotted = false;
                action = IS_IDLING;
                busy = false;
            }
        }
    }

    float getDistance(GameObject target)
    {
        return (Math.Abs(target.transform.position.x - transform.position.x) + Math.Abs(target.transform.position.z - transform.position.z));
    }

    void followPlayer(GameObject player)
    {
        busy = true;
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
        busy = true;
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
        if (spawnPos.x != 0 && !GetComponent<EnemyWander>().busy)
        {
            action = IS_RUNNING;
            transform.position = Vector3.MoveTowards(transform.position, spawnPos, speed * Time.deltaTime);
            var _direction = (spawnPos - transform.position).normalized;
            var _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * speed);
        }
    }

    public void isRunning(bool isRunning)
    {
        animations.isRunning(isRunning);
    }

    public void isIdling(bool isIdling)
    {
        animations.isIdling(isIdling);
    }

    public void isAttacking(bool isAttacking)
    {
        animations.isAttacking(isAttacking);
    }

    public void isWalking(bool isWalking)
    {
        animations.isWalking(isWalking);
    }


}
