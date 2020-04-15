using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : EnemyController
{
    public GameObject healthBar;
    float timer;
    public GameObject targetSprite;
    public float health;
    bool dead = false;
    bool counted;
    void Start()
    {
        counted = false;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        isDead();
        if (dead && !counted)
        {
            CombatEventHandler.Instance.onEnemyDeath(this.xp, this.id);
            CombatEventHandler.Instance.afterEnemyDeath();
            counted = true;
        }
    }

    public void doDamage(float amount)
    {
        health -= amount;
        transform.GetComponent<Animator>().SetTrigger("isDamaged");
        healthBar.GetComponent<Slider>().value = health;
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
            if(transform.GetComponent<EnemyWander>() != null)
                transform.GetComponent<EnemyWander>().enabled = false;
            
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
