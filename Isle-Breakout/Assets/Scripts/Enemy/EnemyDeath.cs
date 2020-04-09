using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public bool dead;
    bool waiting;
    float deathTime;

    void Start()
    {
        waiting = true;
        dead = false;
    }
    void Update()
    {
        if(dead && waiting)
        {
            deathTime = Time.time;
            waiting = false;
        }
        else if(dead && (Time.time - deathTime >= 2))
        {
            Destroy(gameObject);
        }
    }
}
