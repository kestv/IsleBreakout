using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    [SerializeField]GameObject player;
    [SerializeField]GameObject hpText;

    [SerializeField]float maxHealth;
    [SerializeField]float currentHealth;
    float lastVisible;
    Slider healthbarValue;
    public void Start()
    {
        healthbarValue = GetComponent<Slider>();
        healthbarValue.maxValue = maxHealth;
        currentHealth = maxHealth;
        healthbarValue.value = currentHealth;
    }
    public void Update()
    {
        hpText.GetComponent<Text>().text = string.Format("{0}/{1}", currentHealth, maxHealth);
    }
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        healthbarValue.value = currentHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void ChangeMaxHealth(float value)
    {
        maxHealth = value;
        healthbarValue.maxValue = maxHealth;
    }

    public void IncreaseMaxHealth(float value)
    {
        maxHealth += value;
        healthbarValue.maxValue = maxHealth;
    }

    public void Heal(float value)
    {
        currentHealth += value;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthbarValue.value = currentHealth;
    }

    public void SetTarget(GameObject target)
    {
        
        maxHealth = target.GetComponent<EnemyHealthController>().GetMaxHealth();
        currentHealth = target.GetComponent<EnemyHealthController>().GetHealth();
        GetComponent<Slider>().maxValue = maxHealth;
        GetComponent<Slider>().value = currentHealth;
        gameObject.SetActive(true);
        player = target;
    }
    
    public void SetCurrentHealth(float health)
    {
        this.currentHealth = health;
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}