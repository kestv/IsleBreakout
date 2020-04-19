using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public GameObject player;
    public GameObject hpText;

    public float maxHealth;
    public float currentHealth;
    float lastVisible;
    Slider healthbarValue;
    public void Start()
    {
        healthbarValue = GetComponent<Slider>();
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
        healthbarValue.value = currentHealth;
    }
}