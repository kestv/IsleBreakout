using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmRescueQuest : Quest
{
    public void Start()
    {
        this.init(GetComponent<NpcController>().ID);
        this.goals.Add(new KillGoal("Kill all enemies", 3, 3));
        this.Experience = 100;

        this.questGiverId = GetComponent<NpcController>().ID;
    }
}
