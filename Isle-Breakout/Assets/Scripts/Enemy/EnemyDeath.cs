using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    bool dead;
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
            GetComponent<AudioManager>().Play("Death");
        }
        else if(dead && (Time.time - deathTime >= 4))
        {
            Destroy(gameObject);
        }
    }

    public void SetToDead(bool dead)
    {
        this.dead = dead;
    }
}
