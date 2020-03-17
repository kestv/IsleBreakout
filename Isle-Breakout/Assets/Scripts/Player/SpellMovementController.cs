using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMovementController : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 10f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.transform.tag == "Enemy")
        //{
        //    Destroy(gameObject);
        //}
    }
}
