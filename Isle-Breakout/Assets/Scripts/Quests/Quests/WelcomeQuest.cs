using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeQuest : Quest
{

    public void Start()
    {
        this.init(GetComponent<NpcController>().ID);
        this.goals.Add(new TalkGoal("Starting quest", false, 1));
        this.Experience = 50;

        this.goals.ForEach(g => g.Init());


    }
}
