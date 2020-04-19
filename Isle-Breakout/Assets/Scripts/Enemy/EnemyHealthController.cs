using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : EnemyController
{
    public GameObject healthBar;
    float timer;
    public GameObject targetSprite;
    public float maxHealth;
    public float health;
    public bool dead;
    bool counted;
    public PlayerCombatController playerCtrl;
    void Start()
    {
        health = maxHealth;
        playerCtrl = GameObject.Find("PlayerInstance").GetComponent<PlayerCombatController>();
        dead = false;
        counted = false;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        isDead();
    }

    public void doDamage(float amount)
    {
        health -= amount;
        transform.GetComponent<Animator>().SetTrigger("isDamaged");
        healthBar.GetComponent<Slider>().value = health;
        healthBar.GetComponent<HealthController>().currentHealth = health;
        if (!GetComponent<EnemyActionController>().playerSpotted) GetComponent<EnemyActionController>().GotAttacked();
        UIEventHandler.Instance.DisplayDamage(amount);
    }

    public float getHealth()
    {
        return health;
    }

    public bool isDead()
    {
        if (health <= 0)
        {
            GetComponent<EnemyDeath>().dead = true;
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
            return true;
        }
        else return false;
    }

    public GameObject getHealthbarCanvas()
    {
        return healthBar;
    }
}
