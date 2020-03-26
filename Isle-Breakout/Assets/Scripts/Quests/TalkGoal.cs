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
    }

    public override void Init()
    {
        NpcEventHandler.Instance.onTalkedToNpc += TalkedToNpc;
        base.Init();
    }

    public void TalkedToNpc(int id)
    {
        if(this.npcId == id)
            this.Complete();
    }
}
