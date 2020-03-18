using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public GameObject healthBarCanvas;
    public Slider hungerBar;
    public Slider warmthBar;
    bool gettingWarm;
    float timer;
    void Start()
    {
        gettingWarm = false;
        hungerBar = GameObject.Find("Hunger").GetComponent<Slider>();
        warmthBar = GameObject.Find("Warmth").GetComponent<Slider>();
        timer = Time.time;
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
        hungerBar.value -= 0.1f;
        if (gettingWarm == false)
        {
            warmthBar.value -= 0.1f;
        }
        else
        {
            warmthBar.value += 0.5f;
        }
        if(warmthBar.value <= 0 || hungerBar.value <= 0)
        {
            if(Time.time - timer >= 10)
            {
                doDamage(5);
                timer = Time.time;
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
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Campfire")
        {
            gettingWarm = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Campfire")
        {
            gettingWarm = false;
        }
    }
}
