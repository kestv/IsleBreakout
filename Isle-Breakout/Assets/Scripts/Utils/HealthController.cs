using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public GameObject healthBar;
    float lastVisible;
    public void Start()
    {
        lastVisible = Time.time;
        healthBar.SetActive(true);
    }
    public void Update()
    {
        if (Time.time < lastVisible + 1.5f)
            healthBar.SetActive(true);
        //else healthBar.SetActive(false);
    }
    public void setHealth(float amount)
    {
        healthBar.GetComponent<Slider>().value = amount;
        lastVisible = Time.time;
    }

    public float getHealth()
    {
        return healthBar.GetComponent<Slider>().value;
    }
}
