using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
