using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest
{
    int enemyId;
    int reqAmount;
    public void Start()
    {
        InitializeQuest();
    }

    void InitializeQuest()
    {
        this.Init(GetComponent<NPC>().GetId());
        this.goals.Add(new KillGoal(reqAmount, enemyId));
        this.questGiverId = GetComponent<NPC>().GetId();
    }
}
