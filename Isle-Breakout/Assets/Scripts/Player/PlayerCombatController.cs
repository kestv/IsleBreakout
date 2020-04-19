using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatController : MonoBehaviour
{
    bool isTriggering;
    public bool isAttacking;
    public bool inCombat;
    //Attack cooldown
    float lastAttack = 0;
    //Attack rate
    float attackRate = 2.0f;
    //Damage that enemy does
    public float damage;
    GameObject[] enemies;
    public GameObject target;

    SpellController spellController;
    public GameObject arrow;
    public float range;

    bool isRangedWeapon;
    bool isRangedWeaponEquipped;

    GameObject enemyHealthBar;
    GameObject slot1;
    GameObject slot2;
    GameObject slot3;
    GameObject slot4;

    GameObject rangeSelect;
    GameObject meleeSelect;

    GameObject meleeWeapon;
    GameObject rangeWeapon;

    CharacterController controller;

    void Start()
    {
        meleeWeapon = GameObject.Find("MeleeWeapon");
        rangeWeapon = GameObject.Find("RangeWeapon");
        controller = GetComponent<CharacterController>();
        CombatEventHandler.Instance.onEnemyDeath += killedTarget;
        spellController = GetComponent<SpellController>();
        isAttacking = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyHealthBar = GameObject.Find("EnemyHealthbar");
        enemyHealthBar.SetActive(false);
        
        enemyHealthBar.SetActive(false);
        slot1 = GameObject.Find("Slot1");
        slot2 = GameObject.Find("Slot2");
        slot3 = GameObject.Find("Slot3");
        slot4 = GameObject.Find("Slot4");
        
        rangeSelect = GameObject.Find("SelectRange");
        meleeSelect = GameObject.Find("SelectMelee");
        rangeSelect.SetActive(false);
        
        rangeWeapon.SetActive(false);
        inCombat = false;
    }

    void SetWeapon(bool ranged)
    {
        isRangedWeapon = ranged;
        meleeSelect.SetActive(!ranged);
        rangeSelect.SetActive(ranged);
        meleeWeapon.SetActive(!ranged);
        rangeWeapon.SetActive(ranged);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ResetTarget();
            findTarget();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(target == null)
            {
                ResetTarget();
                findTarget();
            }
            else
            {
                inCombat = true;
                isAttacking = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spellController.CastSpell(target, slot1.GetComponent<SpellHolder>());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            spellController.CastSpell(target, slot2.GetComponent<SpellHolder>());
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(isRangedWeaponEquipped)
                SetWeapon(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (target != null)
            {
                target.GetComponent<EnemyHealthController>().targetSprite.SetActive(false);
                target.GetComponent<EnemyHealthController>().nameTag.GetComponent<TextMesh>().color = Color.black;
                enemyHealthBar.SetActive(false);
                target = null;
                inCombat = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
        {
            isAttacking = false;
        }

        if (isAttacking && target != null)
        {
            if (!isRangedWeapon && getDistance(target) > 2f)
            {
                //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 10f * Time.deltaTime);
                var pos = target.transform.position - transform.position;
                pos = pos.normalized * GetComponent<PlayerMovementController>().speed;
                controller.Move(pos * Time.deltaTime);
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
                            target.GetComponent<EnemyHealthController>().doDamage(damage);
                        }
                        else
                        {
                            if (getDistance(target) <= range)
                            {
                                AttackFromRange();
                            }
                        }
                    }
                }
            }
        }
        else transform.GetComponent<PlayerMovementController>().isRunning = false;
    }

    void AttackFromRange()
    {
        transform.GetComponent<Animator>().SetTrigger("isShooting");
        spellController.CastArrow(target, arrow);
    }
    void findTarget()
    {
        float dist = 21;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            var distance = getDistance(enemy);
            if (!enemy.GetComponent<EnemyHealthController>().isDead())
            {
                if (distance < 20 && distance < dist)
                {
                    target = enemy;
                    dist = distance;
                }
                else
                {
                    //target = null;
                    isAttacking = false;
                }
            }
        }
        if (target != null)
        {
            target.GetComponent<EnemyHealthController>().healthBar = enemyHealthBar;
            target.GetComponent<EnemyHealthController>().targetSprite.SetActive(true);
            target.GetComponent<EnemyHealthController>().nameTag.GetComponent<TextMesh>().color = Color.red;
            enemyHealthBar.GetComponent<HealthController>().SetTarget(target);
            
        }
    }

    float getDistance(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    void killedTarget(float xp, int id)
    {
        inCombat = false;
        target.GetComponent<EnemyHealthController>().targetSprite.SetActive(false);
        target.GetComponent<EnemyHealthController>().nameTag.GetComponent<TextMesh>().color = Color.black;
        enemyHealthBar.SetActive(false);
        target = null;
    }

    void ResetTarget()
    {
        enemyHealthBar.SetActive(false);
        enemyHealthBar.GetComponent<HealthController>().player = null;
        if (target != null)
        {
            target.GetComponent<EnemyHealthController>().targetSprite.SetActive(false);
            target.GetComponent<EnemyHealthController>().nameTag.GetComponent<TextMesh>().color = Color.black;
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void setIsRangedWeaponEquipped(bool state)
    { 
        isRangedWeaponEquipped = state;
        var img = slot4.transform.GetChild(1).gameObject.GetComponent<Image>();
        if (state)
        {
            img.color = new Color(255,255,255,255);
        }
        else
        {
            img.color = new Color(0,0,0,100);
        }
    }
}
