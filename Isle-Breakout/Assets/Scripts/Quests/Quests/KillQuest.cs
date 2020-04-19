using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest
{
    public int enemyId;
    public int reqAmount;
    public void Start()
    {
        this.init(GetComponent<NpcController>().ID);
        this.goals.Add(new KillGoal(reqAmount, enemyId));
        this.questGiverId = GetComponent<NpcController>().ID;
    }
}
