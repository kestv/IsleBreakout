using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    const int OFFENSIVE_PET = 1;
    const int DEFENSIVE_PET = 2;

    public int type;
    bool tamed;
    float speed;
    public float damage;
    public float attackCooldown;
    GameObject player;
    EnemyAnimationController animations;
    PlayerCombatController playerCtrl;

    public float bonusStrength;
    public float bonusSpeed;
    public float bonusWisdom;

    float attackTime;
    void Start()
    {
        player = GameObject.Find("PlayerInstance");
        tamed = false;
        animations = GetComponent<EnemyAnimationController>();
        playerCtrl = player.GetComponent<PlayerCombatController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tamed)
        {
            if (type == OFFENSIVE_PET && playerCtrl.inCombat)
            {
                var target = playerCtrl.GetTarget();
                if (target != null)
                {
                    transform.LookAt(target.transform);
                    if (Vector3.Distance(target.transform.position, transform.position) > 3f)
                    {
                        MoveTowardsTarget(target);
                    }
                    else if (Time.time - attackTime > attackCooldown)
                    {
                        AttackTarget(target);
                    }
                    else animations.isIdling(true);
                }
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    public void MoveTowardsTarget(GameObject target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, player.GetComponent<PlayerMovementController>().speed * Time.deltaTime);
        animations.isRunning(true);
    }

    public void AttackTarget(GameObject target)
    {
        animations.isAttacking(true);
        target.GetComponent<EnemyHealthController>().doDamage(damage);
        attackTime = Time.time;
    }

    float GetDistance()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    void FollowPlayer()
    {
        var distance = GetDistance();
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

    public void SetTamed()
    {
        tamed = true;
        GetComponent<EnemyWander>().enabled = false;
        player.GetComponent<PlayerStatsController>().strength += bonusStrength;
        player.GetComponent<PlayerStatsController>().speed += bonusSpeed;
        player.GetComponent<PlayerStatsController>().wisdom += bonusWisdom;
    }

    public void SetUntamed()
    {
        tamed = false;
        GetComponent<EnemyWander>().enabled = true;
        player.GetComponent<PlayerStatsController>().strength -= bonusStrength;
        player.GetComponent<PlayerStatsController>().speed -= bonusSpeed;
        player.GetComponent<PlayerStatsController>().wisdom -= bonusWisdom;
    }
}
