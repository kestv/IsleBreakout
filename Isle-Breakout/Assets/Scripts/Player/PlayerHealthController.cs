using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public GameObject healthBarCanvas;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            transform.GetComponent<Animator>().SetBool("isDead", true);
            if(transform.tag == "Player")
            {
                transform.GetComponent<PlayerMovementController>().enabled = false;
            }
            else if(transform.tag == "Enemy")
            {
                transform.GetComponent<EnemyActionController>().enabled = false;
            }
        }
    }

    public void doDamage(float amount)
    {
        healthBarCanvas.GetComponent<HealthController>().decreaseHealth(amount);
        transform.GetComponent<Animator>().SetTrigger("isDamaged");
    }
    
    public float getHealth()
    {
        return healthBarCanvas.GetComponent<HealthController>().getHealth();
    }

    public bool isDead()
    {
        if (healthBarCanvas.GetComponent<HealthController>().getHealth() <= 0) return true;
        else return false;
    }

    public GameObject getHealthbarCanvas()
    {
        return healthBarCanvas;
    }
}
