using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthController : MonoBehaviour
{
    public GameObject player;
    public float startingHealth;
    public GameObject levelField;
    float lastVisible;
    public GameObject healthBar;
    GameObject obj;
    public void Start()
    {
        obj = gameObject;
        obj.GetComponent<Slider>().value = startingHealth;
    }
    public void Update()
    {
    }
    public void decreaseHealth(float amount)
    {
        obj.GetComponent<Slider>().value -= amount;
    }

    public float getHealth()
    {
        return obj.GetComponent<Slider>().value;
    }
}