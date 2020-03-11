using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    public GameObject item;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PlayerHealthController>().isDead())
        {
            Instantiate(item, transform);
            enabled = false;
        }
    }
}
