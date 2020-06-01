using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField]float speed;
    Light dayLight;
    Light nightLight;
    float startingIntensity;
    float startingNightIntensity;
    [SerializeField]GameObject night;
    void Start()
    {
        nightLight = night.GetComponent<Light>();
        dayLight = GetComponent<Light>();
        startingIntensity = dayLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCycle();   
    }

    void UpdateCycle()
    {
        transform.Rotate(new Vector3(Time.deltaTime * speed, 0, 0));
        if ((transform.eulerAngles.x > 0 && transform.eulerAngles.x < 90) || transform.eulerAngles.x > 355)
        {
            if (dayLight.intensity < startingIntensity)
            {
                dayLight.intensity += Time.deltaTime * speed / 5;

                nightLight.intensity -= Time.deltaTime * speed / 5;
            }
        }
        else
        {
            if (dayLight.intensity > 0.5)
            {
                dayLight.intensity -= Time.deltaTime * speed / 5;

                nightLight.intensity += Time.deltaTime * speed / 5;
            }
        }
    }

    public void setSpeed(float speed)
    { this.speed = speed; }
}
