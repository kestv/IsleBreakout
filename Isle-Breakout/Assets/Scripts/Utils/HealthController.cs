using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public GameObject player;
    public float maxHealth;
    public float currentHealth;
    float lastVisible;
    float healthbarValue;
    public void Start()
    {
        healthbarValue = GetComponent<Slider>().value;
        currentHealth = maxHealth;
        healthbarValue = currentHealth; 
    }
    public void Update()
    {
    }
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        healthbarValue = currentHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void IncreaseMaxHeatlh(float value)
    {
        maxHealth = value;
    }

    public void Heal(float value)
    {
        currentHealth += value;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthbarValue = currentHealth;
    }
}