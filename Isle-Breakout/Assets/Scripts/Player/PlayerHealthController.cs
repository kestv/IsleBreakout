using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    GameObject healthBarCanvas;
    Slider hungerBar;
    float timer;
    float hunger;
    Animator animator;

    UIHealthController healthCtrl;

    void Start()
    {
        healthBarCanvas = GameObject.Find("Healthbar");
        hungerBar = GameObject.Find("Hunger").GetComponent<Slider>();
        healthCtrl = healthBarCanvas.GetComponent<UIHealthController>();
        animator = GetComponent<Animator>();
        hunger = 100;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
        {
            animator.SetBool("isDead", true);
            if(transform.tag == "Player")
            {
                GetComponent<PlayerMovementController>().enabled = false;
                GetComponent<PlayerCombatController>().enabled = false;
                GetComponent<UIController>().EndGame();
                enabled = false;
            }
        }
        hunger -= 0.01f;
        hungerBar.value = hunger;

        if(hungerBar.value <= 0)
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
        if(animator != null)
            animator.SetTrigger("isDamaged");
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

    public void ChangeMaxHealth(float value)
    {
        healthCtrl.ChangeMaxHealth(value);
    }

    public void IncreaseMaxHealth(float value)
    {
        healthCtrl.IncreaseMaxHealth(value);
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

    //TESTS
    public void AssignVariables(Slider slider, float hunger, float health = 100)
    {
        healthCtrl = new UIHealthController();
        healthCtrl.AssignVariables(slider, health);
        this.hunger = hunger;
    }
}
