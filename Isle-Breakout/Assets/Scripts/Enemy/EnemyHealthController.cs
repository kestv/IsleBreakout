using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public GameObject healthBar;
    float timer;
    public GameObject targetSprite;
    public float health;
    bool dead = false;
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBar != null)
        {
            healthBar.GetComponent<Slider>().value = health;
        }
        isDead();
    }

    public void doDamage(float amount)
    {
        health -= amount;
        transform.GetComponent<Animator>().SetTrigger("isDamaged");
        if (!GetComponent<EnemyActionController>().playerSpotted) GetComponent<EnemyActionController>().playerSpotted = true;
    }

    public float getHealth()
    {
        return health;
    }

    public bool isDead()
    {
        if (health <= 0)
        {
            transform.GetComponent<Animator>().SetBool("isDead", true);
            transform.GetComponent<EnemyActionController>().enabled = false;
            transform.GetComponent<EnemyHealthController>().enabled = false;
            if(!dead)
                CombatEventHandler.Instance.onEnemyDeath(GetComponent<EnemyController>().xp);
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
