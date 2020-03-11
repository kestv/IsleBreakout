using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthController : MonoBehaviour
{
    public GameObject player;
    public float startingHealth;
    public GameObject healthBar;
    public GameObject levelField;
    float lastVisible;
    public void Start()
    {
        lastVisible = Time.time;
        healthBar.SetActive(false);
        healthBar.GetComponent<Slider>().value = startingHealth;
    }
    public void Update()
    {
        if (Time.time < lastVisible + 1.5f)
            healthBar.SetActive(true);
        else healthBar.SetActive(false);

        Vector3 pos = player.transform.position;
        transform.position = new Vector3(pos.x, pos.y + 3, pos.z);
    }
    public void decreaseHealth(float amount)
    {
        healthBar.GetComponent<Slider>().value -= amount;
        lastVisible = Time.time;
    }

    public float getHealth()
    {
        return healthBar.GetComponent<Slider>().value;
    }
}