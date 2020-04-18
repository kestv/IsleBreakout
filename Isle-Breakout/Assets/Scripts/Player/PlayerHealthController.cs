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
    float hunger;

    HealthController healthCtrl;

    void Start()
    {
        healthBarCanvas = GameObject.Find("Healthbar");
        hungerBar = GameObject.Find("Hunger").GetComponent<Slider>();
        warmthBar = GameObject.Find("Warmth").GetComponent<Slider>();
        healthCtrl = healthBarCanvas.GetComponent<HealthController>();
        hunger = 100;
        gettingWarm = false;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            transform.GetComponent<Animator>().SetBool("isDead", true);
            if(transform.tag == "Player")
            {
                transform.GetComponent<PlayerMovementController>().enabled = false;
                transform.GetComponent<PlayerCombatController>().enabled = false;
            }
        }
        hunger -= 0.02f;
        hungerBar.value = hunger;

        if(warmthBar.value <= 0 || hungerBar.value <= 0)
        {
            if(Time.time - timer >= 10)
            {
                DoDamage(5);
                timer = Time.time;
            }
        }
    }

    public void DoDamage(float amount)
    {
        healthCtrl.DecreaseHealth(amount);
        transform.GetComponent<Animator>().SetTrigger("isDamaged");
    }
    
    public float GetHealth()
    {
        return healthCtrl.GetHealth();
    }

    public bool IsDead()
    {
        if (healthCtrl.GetHealth() <= 0) return true;
        else return false;
    }

    public GameObject GetHealthbarCanvas()
    {
        return healthBarCanvas;
    }

    public void IncreaseMaxHealth(float value)
    {
        healthCtrl.IncreaseMaxHeatlh(value);
    }

    public void Heal(float value)
    {
        healthCtrl.Heal(value);
    }

    public void Eat(float value)
    {
        hunger += value;
        if(hunger > 100)
        {
            var remainder = hunger - 100;
            hunger = 100;
            Heal(remainder);
        }

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
