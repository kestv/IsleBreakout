using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    const int OFFENSIVE_PET = 1;
    const int DEFENSIVE_PET = 2;

    [SerializeField]int type;
    bool tamed;
    float speed;
    [SerializeField]float damage;
    [SerializeField]float attackCooldown;
    GameObject player;
    EnemyAnimationController animations;
    PlayerCombatController playerCtrl;

    [SerializeField]float bonusStrength;
    [SerializeField]float bonusSpeed;
    [SerializeField]float bonusWisdom;
    [SerializeField]float bonusHealth;

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
            if (type == OFFENSIVE_PET && playerCtrl.GetInCombat())
            {
                var target = playerCtrl.GetTarget();
                if (target != null)
                {
                    transform.LookAt(target.transform);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                    if (Vector3.Distance(target.transform.position, transform.position) > 3f)
                    {
                        MoveTowardsTarget(target);
                    }
                    else if (Time.time - attackTime > attackCooldown)
                    {
                        AttackTarget(target);
                    }
                    else animations.IsIdling(true);
                }
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    void MoveTowardsTarget(GameObject target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, player.GetComponent<PlayerMovementController>().GetSpeed() * Time.deltaTime);
        animations.IsRunning(true);
    }

    void AttackTarget(GameObject target)
    {
        animations.IsAttacking(true);
        target.GetComponent<EnemyHealthController>().DoDamage(damage);
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
            animations.IsIdling(true);
        }
        else if (distance > 6 && distance <= 10)
        {
            speed = 3f;
            transform.LookAt(player.transform);
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            animations.IsWalking(true);
        }
        else
        {
            speed = player.GetComponent<PlayerMovementController>().GetSpeed() - 1;
            transform.LookAt(player.transform);
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            animations.IsRunning(true);
        }
    }

    public void SetTamed()
    {
        tamed = true;
        GetComponent<EnemyWander>().enabled = false;
        player.GetComponent<PlayerStatsController>().AddBonuses(bonusStrength, bonusSpeed, bonusWisdom);
    }

    public void SetUntamed()
    {
        tamed = false;
        GetComponent<EnemyWander>().enabled = true;
        player.GetComponent<PlayerStatsController>().AddBonuses(-bonusStrength, -bonusSpeed, -bonusWisdom);
    }

    public bool IsTamed()
    {
        return this.tamed;
    }
}
