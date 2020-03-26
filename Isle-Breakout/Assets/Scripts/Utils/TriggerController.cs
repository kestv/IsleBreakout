using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public GameObject info;
    bool triggeringNpc = false;
    int villager = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggeringNpc && Input.GetKeyDown(KeyCode.F))
        {
            NpcEventHandler.Instance.onTalkedToNpc(villager);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Npc")
        {
            triggeringNpc = true;
            if (col.gameObject.name == "Villager") villager = 1;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Npc")
        {
            triggeringNpc = false;
        }
    }
}
