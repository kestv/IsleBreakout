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
    public bool Completed;
    //TODO add items reward

    public GameObject player;

    public void Start()
    {
        player = GameObject.Find("PlayerInstance");
    }
    public void CheckGoals()
    {
        if(goals.All(x => x.completed == true))
        {
            Completed = true;
            GiveReward();
        }
    }

    public void GiveReward()
    {
        if(Experience != 0)
            player.GetComponent<PlayerLevelController>().GetExperience(Experience);
    }

    public void Update()
    {
        CheckGoals();
    }
}
