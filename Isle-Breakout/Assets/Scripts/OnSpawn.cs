using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSpawn : MonoBehaviour
{
    float timer;
    void Start()
    {
        timer = Time.time;
        GetComponent<Animator>().SetTrigger("FadeOut");
    }

    private void Update()
    {
        if(Time.time - timer > 2.5f)
        {
            gameObject.SetActive(false);
        }
    }
}
