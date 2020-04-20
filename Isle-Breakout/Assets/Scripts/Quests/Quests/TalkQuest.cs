using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuest : Quest
{
    public int npcIdToTalk;
    public void Start()
    {
        this.init(GetComponent<NpcController>().ID);
        this.goals.Add(new TalkGoal(false, npcIdToTalk));
    }
}
