using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionController : MonoBehaviour
{
    float speed = 5f;
    GameObject player;
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

    //Attack cooldown
    float lastAttack = 0;

    //Damage that enemy does
    [SerializeField]
    float damage;

    //Attack rate
    [SerializeField]
    float attackRate = 3.0f;

    [SerializeField]
    const float fallBackDistance = 30f;
    const float fallBackTime = 3f;
    float fallBackStart;
    bool gotAttacked;
    float attackTime;
    float followTime;
    bool busy;
    [SerializeField]
    float scanDistance = 8;

    EnemyAnimationController animations;
    EnemyWander wander;
    AudioManager audioManager;

    //Performance
    bool canStartWandering = false;
    bool playerInRange = false;

    void Start()
    {
        gotAttacked = false;
        animations = GetComponent<EnemyAnimationController>();
        wander = GetComponent<EnemyWander>();
        audioManager = GetComponent<AudioManager>();
        busy = false;
        player = GameObject.Find("PlayerInstance");
        playerSpotted = false;
        StartCoroutine(IELanding());
        InvokeRepeating("CheckIfInRange", 0.0f, 1.0f);
    }

    IEnumerator IELanding()
    {
        yield return new WaitForSeconds(5f);
        spawnPos = transform.position;
        canStartWandering = true;
        attackTime = 0f;
    }

    //Performance
    void CheckIfInRange()
    {
        playerInRange = GetDistance(player) < 200 ? true : false;
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
                        IsIdling(true);
                        break;
                    case 2:
                        IsRunning(true);
                        break;
                    case 3:
                        IsAttacking(true);
                        break;
                }
            }

            //If idling, looking for action
            if (!playerSpotted && Time.time - fallBackStart > fallBackTime)
            {
                action = IS_IDLING;
                distance = GetDistance(player);
                if (distance < scanDistance)
                {
                    attackTime = Time.time;
                    followTime = UnityEngine.Random.Range(3, 6);
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
                FollowPlayer(player);
            }
            else
            {
                gotAttacked = false;
            }

            //Running too far loses target
            if (playerSpotted && Time.time - attackTime > followTime)
            {
                fallBackStart = Time.time;
                playerSpotted = false;
                busy = true;
                print("lost target");
            }

            //Normal situation, player spotted
            if ((playerSpotted && !player.GetComponent<PlayerHealthController>().IsDead()))
            {
                FollowPlayer(player);
                if (wander != null)
                    wander.enabled = false;
                if (GetDistance(player) > fallBackDistance && !gotAttacked)
                {
                    playerSpotted = false;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, spawnPos) > 0.5f && !wander.IsBusy())
                {
                    playerSpotted = false;
                    busy = false;
                    GoBackToCamp();
                }
                else
                //if (Vector3.Distance(transform.position, spawnPos) <= 0.5f)
                {
                    playerSpotted = false;
                    if(wander != null && !wander.IsBusy())
                        IsIdling(true);
                    busy = false;
                    if (wander != null)
                        wander.enabled = true;
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

    float GetDistance(GameObject target)
    {
        return (Math.Abs(target.transform.position.x - transform.position.x) + Math.Abs(target.transform.position.z - transform.position.z));
    }

    void FollowPlayer(GameObject player)
    {
        busy = true;
        if (!player.GetComponent<PlayerHealthController>().IsDead())
        {
            transform.LookAt(player.transform);
            if (GetDistance(player) > 2.5f)
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
                Attack(player);
            }
        }
    }

    void Attack(GameObject target)
    {
        busy = true;
        if (lastAttack + attackRate < Time.time)
        {
            lastAttack = Time.time;
            attackTime = lastAttack;
            action = IS_ATTACKING;
            target.GetComponent<PlayerHealthController>().DoDamage(damage);
            audioManager.Play("Punch");
        }
        else action = IS_IDLING;
    }

    void GoBackToCamp()
    {
        print("going to camp");
        var busy = GetComponent<EnemyWander>() ? GetComponent<EnemyWander>().IsBusy() : false;
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

    public void IsRunning(bool isRunning)
    {
        animations.IsRunning(isRunning);
        
    }

    public void IsIdling(bool isIdling)
    {
        animations.IsIdling(isIdling);
        
    }

    public void IsAttacking(bool isAttacking)
    {
        animations.IsAttacking(isAttacking);
    }

    public void IsWalking(bool isWalking)
    {
        animations.IsWalking(isWalking);
    }

    public bool IsPlayerSpotted()
    {
        return this.playerSpotted;
    }

    public bool IsBusy()
    {
        return this.busy;
    }

    public float GetSpeed()
    {
        return speed;
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

    //TESTS
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
