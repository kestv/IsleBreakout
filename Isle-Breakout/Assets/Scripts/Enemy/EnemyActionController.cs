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
    public Vector3 spawnPos;

    //Trigger if player was found
    public bool playerSpotted;

    //Attack cooldown
    float lastAttack = 0;

    //Damage that enemy does
    public float damage;

    //Attack rate
    float attackRate = 2.0f;

    public float fallBackDistance = 30f;
    public bool gotAttacked;
    float attackTime;
    float followTime;
    public bool busy;
    public float scanDistance = 8;

    public EnemyAnimationController animations;
    EnemyWander wander;

    //Performance
    bool canStartWandering = false;
    bool playerInRange = false;

    void Start()
    {
        gotAttacked = false;
        animations = GetComponent<EnemyAnimationController>();
        wander = GetComponent<EnemyWander>();
        busy = false;
        player = GameObject.Find("PlayerInstance");
        playerSpotted = false;
        StartCoroutine(Landing());
        InvokeRepeating("CheckIfInRange", 0.0f, 1.0f);
    }

    IEnumerator Landing()
    {
        yield return new WaitForSeconds(5f);
        spawnPos = transform.position;
        canStartWandering = true;
        attackTime = 0f;
    }

    //Performance
    void CheckIfInRange()
    {
        playerInRange = getDistance(player) < 100 ? true : false;
    }

    void Update()
    {
        if (playerInRange)
        {
            if(wander != null && !wander.enabled && canStartWandering)
            {
                wander.enabled = true;
            }
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

            //If idling, looking for action
            if (!playerSpotted)
            {
                action = IS_IDLING;
                distance = getDistance(player);
                if (distance < scanDistance)
                {
                    attackTime = Time.time;
                    followTime = UnityEngine.Random.Range(5, 10);
                    busy = true;
                    playerSpotted = true;
                }
                else
                {
                    playerSpotted = false;
                }
            }

            //If it was attacked by player
            if (gotAttacked && Time.time - attackTime < followTime)
            {
                playerSpotted = true;
                followPlayer(player);
            }
            else
            {
                gotAttacked = false;
            }

            //Running too far loses target
            if (playerSpotted && Time.time - attackTime > followTime)
            {
                playerSpotted = false;
            }

            //Normal situation, player spotted
            if ((playerSpotted && !player.GetComponent<PlayerHealthController>().IsDead()))
            {
                followPlayer(player);
                if (getDistance(player) > fallBackDistance && !gotAttacked)
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
                else
                //if (Vector3.Distance(transform.position, spawnPos) <= 0.5f)
                {
                    playerSpotted = false;
                    isIdling(true);
                    busy = false;
                }
            }
        }
        else
        {
            if(wander != null && wander.enabled)
            {
                wander.enabled = false;
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
        if (!player.GetComponent<PlayerHealthController>().IsDead())
        {
            transform.LookAt(player.transform);
            if (getDistance(player) > 2.5f)
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
                attack(player);
            }
        }
    }

    void attack(GameObject target)
    {
        busy = true;
        if (lastAttack + attackRate < Time.time)
        {
            lastAttack = Time.time;
            attackTime = lastAttack;
            action = IS_ATTACKING;
            target.GetComponent<PlayerHealthController>().DoDamage(damage);
        }
        else action = IS_IDLING;
    }

    void goBackToCamp()
    {
        var busy = GetComponent<EnemyWander>() ? GetComponent<EnemyWander>().busy : false;
        if (spawnPos.x != 0 && !busy)
        {
            action = IS_RUNNING;
            transform.position = Vector3.MoveTowards(transform.position, spawnPos, speed * Time.deltaTime);
            var _direction = (spawnPos - transform.position).normalized;
            var _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * speed);
        }
    }

    public void GotAttacked()
    {
        gotAttacked = true;
        playerSpotted = true;
        attackTime = Time.time;
        followTime = UnityEngine.Random.Range(1, 5);
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag.Equals("Player"))
    //    {
    //        playerInRange = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag.Equals("Player"))
    //    {
    //        playerInRange = false;
    //    }
    //}
}
