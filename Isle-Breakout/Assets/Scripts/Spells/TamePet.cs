using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamePet : MonoBehaviour
{
    bool skillAcquired;
    bool triggering;
    GameObject pet;
    void Start()
    {
        skillAcquired = true;
        triggering = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            pet.GetComponent<PetController>().tamed = true;
            pet.GetComponent<EnemyWander>().enabled = false;
            Debug.Log("Tamed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pet")
        {
            pet = other.gameObject;
            triggering = true;
        }
    }
}
