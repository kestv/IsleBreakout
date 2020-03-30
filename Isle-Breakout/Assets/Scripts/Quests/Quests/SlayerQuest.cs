using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayerQuest : Quest
{
    public void Start()
    {
        this.init(GetComponent<NpcController>().ID);
        this.goals.Add(new KillGoal("Kill 5 enemies", false, 0, 3, 1));
        this.Experience = 50;

        this.goals.ForEach(g => g.Init());

        this.questGiverId = GetComponent<NpcController>().ID;
    }
}
