using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenPrisoners : Quest
{
    public int npcIdToTalk;
    public void Start()
    {
        this.init(GetComponent<NpcController>().ID);
        this.goals.Add(new TalkGoal("Prisoners quest", false, npcIdToTalk));
    }
}
