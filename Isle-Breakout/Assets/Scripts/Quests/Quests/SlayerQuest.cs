using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayerQuest : Quest
{
    public void Start()
    {
        this.init(GetComponent<NpcController>().ID);
        this.goals.Add(new KillGoal("Kill 5 enemies", 3, 3));
        this.Experience = 50;

        this.questGiverId = GetComponent<NpcController>().ID;
    }
}
