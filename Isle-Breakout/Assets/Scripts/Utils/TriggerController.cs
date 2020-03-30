using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public GameObject info;
    bool triggeringNpc = false;
    int ID = 0;
    List<string> conversations;
    List<Quest> quests;
    string npcName;

    // Update is called once per frame
    void Update()
    {
        if(triggeringNpc && Input.GetKeyDown(KeyCode.F))
        {
            NpcEventHandler.Instance.onTalkedToNpc(ID, conversations, npcName, quests);
            if (NpcEventHandler.Instance._onTalkedToNpc != null)
            {
                Debug.Log("calling");
                NpcEventHandler.Instance._onTalkedToNpc();
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Npc")
        {
            if (col.gameObject.GetComponent<NpcController>() != null)
            {
                triggeringNpc = true;
                ID = col.gameObject.GetComponent<NpcController>().ID;
                conversations = col.gameObject.GetComponent<NpcController>().conversations;
                npcName = col.gameObject.GetComponent<NpcController>().name;
                quests = col.gameObject.GetComponent<NpcController>().quests;
            }

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
