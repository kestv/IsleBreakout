using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    Vector3 startPosition;
    bool active;
    float startTime = 0;
    void Start()
    {
        active = false;
        startPosition = transform.position;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (active)
        {
            if (Time.time - startTime < 3)
            {
                transform.Translate(new Vector3(0, -5f * Time.deltaTime, 0));
                //TODO fade
                //var color = GetComponent<Text>().color;
                //color = new Color(color.r, color.g, color.b, color.a - Time.deltaTime);
            }
            else
            {
                gameObject.SetActive(false);
                active = false;
            }
        }
        
    }

    public void StartMoving()
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
        startTime = Time.time;
        active = true;
    }
}
