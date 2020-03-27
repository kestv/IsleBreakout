using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    public List<Goal> goals = new List<Goal>();
    public string Name;
    public string Description;
    public int Experience;
    public bool Completed = false;
    public bool Active = false;
    //TODO add items reward

    public GameObject player;

    public void Awake()
    {
        player = GameObject.Find("PlayerInstance");
        NpcEventHandler.Instance.afterTalkedToNpc += CheckGoals;
    }
    public void CheckGoals()
    {
        Debug.Log("CheckGoals");
        if(goals.All(x => x.completed == true))
        {
            if (Active && !Completed)
            {
                GiveReward();
                Completed = true;
            }
        }
    }

    public void GiveReward()
    {
        Debug.Log("reward given");
        if(Experience != 0)
            player.GetComponent<PlayerLevelController>().GetExperience(Experience);
    }
}
