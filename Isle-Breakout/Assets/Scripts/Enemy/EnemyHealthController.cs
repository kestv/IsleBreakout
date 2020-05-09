using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : EnemyController
{
    [SerializeField]
    GameObject healthBar;
    float timer;
    [SerializeField]
    GameObject targetSprite;
    [SerializeField]
    float maxHealth;
    float health;
    bool dead;
    bool counted;
    PlayerCombatController playerCtrl;
    void Start()
    {
        this.nameTag.GetComponent<TextMesh>().text = this._name;
        health = maxHealth;
        playerCtrl = GameObject.Find("PlayerInstance").GetComponent<PlayerCombatController>();
        dead = false;
        counted = false;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        IsDead();
        CheckForNameTag();
    }
    float GetDistance(Transform target)
    {
        return (Math.Abs(target.position.x - transform.position.x) + Math.Abs(target.position.z - transform.position.z));
    }
    void CheckForNameTag()
    {
        if(GetDistance(playerCtrl.gameObject.transform) > 50 && nameTag.activeSelf == true)
        {
            nameTag.SetActive(false);
        }
        else if(GetDistance(playerCtrl.gameObject.transform) < 50 && nameTag.activeSelf == false)
        {
            nameTag.SetActive(true);
        }
    }

    public void DoDamage(float amount)
    {
        health -= amount;
        transform.GetComponent<Animator>().SetTrigger("isDamaged");
        healthBar.GetComponent<Slider>().value = health;
        healthBar.GetComponent<UIHealthController>().SetCurrentHealth(health);
        if (!GetComponent<EnemyActionController>().IsPlayerSpotted()) GetComponent<EnemyActionController>().GotAttacked();
        UIEventHandler.Instance.DisplayDamage(amount);
    }

    public float GetHealth()
    {
        return health;
    }

    public bool IsDead()
    {
        if (health <= 0)
        {
            GetComponent<EnemyDeath>().SetToDead(true);
            transform.GetComponent<Animator>().SetBool("isDead", true);
            transform.GetComponent<EnemyActionController>().enabled = false;
            transform.GetComponent<EnemyHealthController>().enabled = false;
            if (transform.GetComponent<EnemyWander>() != null)
                transform.GetComponent<EnemyWander>().enabled = false;
            if (!dead)
            {
                CombatEventHandler.Instance.onEnemyDeath(this.xp, this.id);
                CombatEventHandler.Instance.afterEnemyDeath();
            }
            dead = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
            return true;
        }
        else return false;
    }

    public GameObject GetHealthbarCanvas()
    {
        return healthBar;
    }

    public GameObject GetTargetSprite()
    {
        return this.targetSprite;
    }

    public float GetMaxHealth()
    {
        return this.maxHealth;
    }

    public void SetHealthBar(GameObject healthBar)
    {
        this.healthBar = healthBar;
    }
}
