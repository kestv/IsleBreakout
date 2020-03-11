using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatController : MonoBehaviour
{
    bool isTriggering;
    GameObject enemy;
    
    //Attack cooldown
    float lastAttack = 0;
    //Attack rate
    float attackRate = 2.0f;
    //Damage that enemy does
    public float damage;
    public int level;
    float experiencePoints;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        levelUp();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            attack();
        }
    }

    void attack()
    {
        damage = GetComponent<PlayerStatsController>().strength * 10 + damage;
        if (isTriggering)
        {
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            float dotProd = Vector3.Dot(direction, transform.forward);

            if (dotProd > 0.95)
            {
                if (lastAttack + attackRate < Time.time && !enemy.GetComponent<PlayerHealthController>().isDead())
                {
                    lastAttack = Time.time;
                    transform.GetComponent<Animator>().SetTrigger("isAttacking");
                    enemy.GetComponent<PlayerHealthController>().doDamage(damage);
                    if(enemy.GetComponent<PlayerHealthController>().isDead())
                    {
                        killedTarget();
                    }
                }
            }
        }
    }

    void killedTarget()
    {
        levelUp();
    }

    void levelUp()
    {
        level++;
        var hpCanvas = GetComponent<PlayerHealthController>().getHealthbarCanvas();
        hpCanvas.GetComponent<HealthController>().levelField.GetComponent<Text>().text = level.ToString();
        GetComponent<PlayerStatsController>().remainingPoints++;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Enemy")
        {
            isTriggering = true;
            enemy = collider.gameObject;
        }
        else
        {
            enemy = null;
            isTriggering = false;
        }
    }
}
