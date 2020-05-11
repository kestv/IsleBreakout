using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesScroller : MonoBehaviour
{
    Vector3 startPosition;
    bool active;
    float startTime = 0;
    void Start()
    {
        active = false;
        startPosition = transform.localPosition;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (active)
        {
            Move();
        }
        
    }

    public void StartMoving()
    {
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(transform.localPosition.x, startPosition.y);
        startTime = Time.time;
        active = true;
    }

    void Move()
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
