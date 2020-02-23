using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float startX;
    float startZ;
    bool goLeft = true;

    public AnimationClip walkAnimation;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(startX + 10 == Math.Round(transform.position.x))
        {
            goLeft = true;
            transform.Rotate(new Vector3(0, 180, 0));
            
        }
        else if(startX == Math.Round(transform.position.x))
        {
            goLeft = false;
            transform.Rotate(new Vector3(0, 180, 0));
        }

        if (goLeft)
        {
            transform.Translate(new Vector3(5f * Time.deltaTime, 0));
        }
        else
        {
            transform.Translate(new Vector3(-5f * Time.deltaTime, 0));
        }
    }
}
