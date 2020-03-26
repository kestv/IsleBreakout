using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatController : MonoBehaviour
{
    bool isTriggering;
    public bool isAttacking;
    //Attack cooldown
    float lastAttack = 0;
    //Attack rate
    float attackRate = 2.0f;
    //Damage that enemy does
    public float damage;
    GameObject[] enemies;
    public GameObject target;

    SpellController spellController;
    bool isRangedWeapon;

    public GameObject levelField;
    GameObject enemyHealthBar;
    GameObject slot1;
    GameObject slot2;
    GameObject slot3;
    GameObject slot4;

    GameObject rangeSelect;
    GameObject meleeSelect;

    GameObject meleeWeapon;
    GameObject rangeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        spellController = GetComponent<SpellController>();
        isAttacking = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyHealthBar = GameObject.Find("EnemyHealthbar");
        levelField = GameObject.Find("Level");
        enemyHealthBar.SetActive(false);
        Debug.Log(levelField.GetComponent<Text>().text);
        slot1 = GameObject.Find("Slot1");
        slot2 = GameObject.Find("Slot2");
        slot3 = GameObject.Find("Slot3");
        slot4 = GameObject.Find("Slot4");

        rangeSelect = GameObject.Find("SelectRange");
        meleeSelect = GameObject.Find("SelectMelee");
        rangeSelect.SetActive(false);

        meleeWeapon = GameObject.Find("MeleeWeapon");
        rangeWeapon = GameObject.Find("RangeWeapon");
        rangeWeapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            findTarget();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(target == null)
            {
                findTarget();
            }
            else
            {
                isAttacking = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(target != null)
            {
                spellController.castSpell(target, slot1.GetComponent<SpellHolder>());
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (target != null)
            {
                spellController.castSpell(target, slot2.GetComponent<SpellHolder>());
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isRangedWeapon = false;
            meleeSelect.SetActive(true);
            rangeSelect.SetActive(false);
            meleeWeapon.SetActive(true);
            rangeWeapon.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isRangedWeapon = true;
            meleeSelect.SetActive(false);
            rangeSelect.SetActive(true);
            meleeWeapon.SetActive(false);
            rangeWeapon.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (target != null)
            {
                target.GetComponent<EnemyHealthController>().targetSprite.SetActive(false);
                enemyHealthBar.SetActive(false);
                target = null;
            }
        }
        if(target != null && target.GetComponent<EnemyHealthController>().isDead())
        {
            target.GetComponent<EnemyHealthController>().targetSprite.SetActive(false);
            killedTarget();
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
        {
            isAttacking = false;
        }

        if (isAttacking && target != null)
        {
            if (!isRangedWeapon && getDistance(target) > 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 10f * Time.deltaTime);
                transform.GetComponent<PlayerMovementController>().isRunning = true;
            }
            else
            {
                transform.GetComponent<PlayerMovementController>().isRunning = false;
                var damage = GetComponent<PlayerStatsController>().strength * 10 + this.damage;

                Vector3 direction = (target.transform.position - transform.position).normalized;
                float dotProd = Vector3.Dot(direction, transform.forward);

                if (dotProd > 0.95)
                {
                    if (lastAttack + attackRate < Time.time && !target.GetComponent<EnemyHealthController>().isDead())
                    {
                        lastAttack = Time.time;
                        if (!isRangedWeapon)
                        {
                            transform.GetComponent<Animator>().SetTrigger("isAttacking");  
                        }
                        else
                        {
                            transform.GetComponent<Animator>().SetTrigger("isShooting");
                        }
                        target.GetComponent<EnemyHealthController>().doDamage(damage);
                        if (target.GetComponent<EnemyHealthController>().isDead())
                        {
                            target.GetComponent<EnemyHealthController>().targetSprite.SetActive(false);
                            killedTarget();
                        }
                    }
                }
            }
        }
        else transform.GetComponent<PlayerMovementController>().isRunning = false;
    }
    void findTarget()
    {
        foreach (var enemy in enemies)
        {
            if (!enemy.GetComponent<EnemyHealthController>().isDead())
            {
                if (getDistance(enemy) < 20)
                {
                    target = enemy;
                    target.GetComponent<EnemyHealthController>().healthBar = enemyHealthBar;

                    target.GetComponent<EnemyHealthController>().targetSprite.SetActive(true);
                    enemyHealthBar.SetActive(true);
                    enemyHealthBar.GetComponent<HealthController>().player = target;
                    enemyHealthBar.GetComponent<Slider>().value = target.GetComponent<EnemyHealthController>().health;
                    return;
                }
                else
                {
                    target = null;
                    isAttacking = false;
                }
            }
        }
    }

    float getDistance(GameObject target)
    {
        return (Math.Abs(target.transform.position.x - transform.position.x) + Math.Abs(target.transform.position.z - transform.position.z));
    }

    void killedTarget()
    {
        GetComponent<PlayerLevelController>().LevelUp();
        enemyHealthBar.SetActive(false);
        target = null;
    }
}
