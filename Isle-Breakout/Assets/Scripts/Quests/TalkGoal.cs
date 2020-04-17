using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkGoal : Goal
{
    public int npcId;
    public TalkGoal(string description, bool completed, int id)
    {
        this.description = description;
        this.completed = completed;
        this.npcId = id;
        this.currentAmount = 0;
        this.requiredAmount = 1;
    }

    public TalkGoal(string description, bool completed, int id, int requiredAmount)
    {
        this.description = description;
        this.completed = completed;
        this.npcId = id;
        this.currentAmount = 0;
        this.requiredAmount = requiredAmount;
    }

    public override void Init()
    {
        NpcEventHandler.Instance.onTalkedToNpc += TalkedToNpc;
        base.Init();
    }

    public void TalkedToNpc(int id, List<string> conversations, string name, List<Quest> quests)
    {
        Debug.Log("TalkedToNpc");
        if (this.npcId == id)
        {
            if (this.requiredAmount == 1)
            {
                this.currentAmount = 1;
                this.Complete();
            }
            else
                this.currentAmount += 1;
        }
    }
}
