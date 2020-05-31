using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatController : MonoBehaviour
{
    bool isTriggering;
    bool isAttacking;
    bool inCombat;
    bool isCharging;
    //Attack cooldown
    float lastAttack = 0;
    //Attack rate
    float attackRate = 2.0f;
    //Damage that enemy does
    [SerializeField]
    float damage;
    GameObject[] enemies;
    [SerializeField]
    GameObject target;

    SpellController spellController;
    [SerializeField]
    GameObject arrow;
    [SerializeField]
    float range;

    bool isRangedWeapon;
    bool isRangedWeaponEquipped;

    GameObject enemyHealthBar;
    GameObject slot1;
    GameObject slot2;
    GameObject slot3;
    GameObject slot4;

    GameObject rangeSelect;
    GameObject meleeSelect;

    [Header("Weapon refrences on player model")]
    [SerializeField] GameObject meleeWeapon;
    [SerializeField] GameObject rangeWeapon;

    CharacterController controller;
    PlayerMovementController movementCtrl;
    AudioManager audioManager;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        CombatHandler.Instance.onEnemyDeath += KilledTarget;
        spellController = GetComponent<SpellController>();
        movementCtrl = GetComponent<PlayerMovementController>();
        audioManager = GetComponent<AudioManager>();
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
        SetWeapon(false);
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
            FindTarget();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(target == null)
            {
                ResetTarget();
                FindTarget();
            }
            else
            {
                inCombat = true;
                isAttacking = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && Time.timeScale != 0f) //timeScale 0, kai GUI
        {
            spellController.CastSpell(target, slot1.GetComponent<SpellHolder>());
        }
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0f)
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

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
        {
            isAttacking = false;
            movementCtrl.SetCanAnimate(true);
        }

        if (isAttacking && target != null)
        {

            if (!isRangedWeapon && GetDistance(target) > 2f)
            {
                //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 10f * Time.deltaTime);
                var pos = target.transform.position - transform.position;
                pos = pos.normalized * movementCtrl.GetSpeed();
                if (!movementCtrl.GetIsRunning())
                {
                    controller.Move(pos * Time.deltaTime);
                    movementCtrl.SetIsRunning(true);
                    GetComponent<Animator>().SetBool("Running", true);
                    isCharging = true;
                    movementCtrl.SetCanAnimate(false);
                    transform.LookAt(target.transform);
                }
                
            }
            else
            {
                GetComponent<Animator>().SetBool("Running", false);
                isCharging = false;
                movementCtrl.SetIsRunning(false);
                var damage = GetComponent<PlayerStatsController>().GetStrength() * 3 + this.damage;

                Vector3 direction = (target.transform.position - transform.position).normalized;
                float dotProd = Vector3.Dot(direction, transform.forward);

                if (dotProd > 0.6)
                {
                    if (lastAttack + attackRate < Time.time && !target.GetComponent<EnemyHealthController>().IsDead())
                    {
                        lastAttack = Time.time;
                        if (!isRangedWeapon)
                        {
                            StartCoroutine(MeleeAttack(damage));
                        }
                        else
                        {
                            if (GetDistance(target) <= range)
                            {
                                AttackFromRange();
                            }
                        }
                    }
                }
            }
        }
        else movementCtrl.SetIsRunning(false);
    }

    IEnumerator MeleeAttack(float damage)
    {
        transform.GetComponent<Animator>().SetTrigger("isAttacking");
        if (meleeWeapon.activeSelf == true)
        {
            int type = UnityEngine.Random.Range(1, 3);
            audioManager.Play(String.Format("Sword{0}", type));
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            audioManager.Play("Punch");
        }
        target.GetComponent<EnemyHealthController>().DoDamage(damage);
    }

    public void CancelTarget()
    {
        target.GetComponent<EnemyHealthController>().GetTargetSprite().SetActive(false);
        target.GetComponent<EnemyHealthController>().GetNameTag().GetComponent<TextMesh>().color = Color.green;
        enemyHealthBar.SetActive(false);
        target = null;
        inCombat = false;
    }
    public void AttackFromRange()
    {
        transform.GetComponent<Animator>().SetTrigger("isShooting");
        spellController.CastArrow(target, arrow);
    }
    public void FindTarget()
    {
        float dist = 21;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            var distance = GetDistance(enemy);
            if (!enemy.GetComponent<EnemyHealthController>().IsDead())
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
            var eHCtrl = target.GetComponent<EnemyHealthController>();
            eHCtrl.SetHealthBar(enemyHealthBar);
            eHCtrl.GetTargetSprite().SetActive(true);
            eHCtrl.GetNameTag().GetComponent<TextMesh>().color = Color.red;
            enemyHealthBar.GetComponent<UIHealthController>().SetTarget(target);
            
        }
    }

    float GetDistance(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public void KilledTarget(float xp, int id)
    {
        inCombat = false;
        var sprite = target.GetComponent<EnemyHealthController>().GetTargetSprite();
        if (sprite != null && sprite.activeSelf == true)
        {
            sprite.SetActive(false);
        }
        target.GetComponent<EnemyHealthController>().GetNameTag().GetComponent<TextMesh>().color = Color.black;
        enemyHealthBar.SetActive(false);
        target = null;
    }

    public void ResetTarget()
    {
        enemyHealthBar.SetActive(false);
        enemyHealthBar.GetComponent<UIHealthController>().SetPlayer(null);
        if (target != null)
        {
            target.GetComponent<EnemyHealthController>().GetTargetSprite().SetActive(false);
            target.GetComponent<EnemyHealthController>().GetNameTag().GetComponent<TextMesh>().color = Color.black;
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void SetIsRangedWeaponEquipped(bool state)
    { 
        isRangedWeaponEquipped = state;
        var img = slot4.transform.GetChild(1).gameObject.GetComponent<Image>();
        if (state)
        {
            img.color = new Color32(255,255,255,255);
        }
        else
        {
            img.color = new Color32(0,0,0,100);
        }
    }

    public bool GetInCombat()
    {
        return this.inCombat;
    }

    public void SetInCombat(bool inCombat)
    {
        this.inCombat = inCombat;
    }

    public bool IsCharging()
    {
        return isCharging;
    }

    public float GetDamage()
    {
        return this.damage;
    }
}
