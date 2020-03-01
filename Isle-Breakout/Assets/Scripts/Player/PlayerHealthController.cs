using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public float health;
    public GameObject healthBarCanvas;
    void Start()
    {
        healthBarCanvas.GetComponent<HealthController>().setHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doDamage(float amount)
    {
        health -= amount;
        healthBarCanvas.GetComponent<HealthController>().setHealth(health);
    }

    public bool isDead()
    {
        if (healthBarCanvas.GetComponent<HealthController>().getHealth() <= 0) return true;
        else return false;
    }
}
