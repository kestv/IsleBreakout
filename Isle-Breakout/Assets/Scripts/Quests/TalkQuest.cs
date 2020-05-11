using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuest : Quest
{
    [SerializeField]int npcIdToTalk;
    public void Start()
    {
        InitializeQuest();
    }

    void InitializeQuest()
    {
        this.Init(GetComponent<NPC>().GetId());
        this.goals.Add(new TalkGoal(false, npcIdToTalk));
    }
}
