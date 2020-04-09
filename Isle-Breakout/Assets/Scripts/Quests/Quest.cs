using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public int questGiverId;
    public List<Goal> goals = new List<Goal>();
    public List<string> conversations = new List<string>();
    public int Experience;
    public string Name;
    public bool Completed = false;
    public bool Active = false;

    public GameObject spellReward;
    public GameObject itemReward;

    public GameObject questName;
    public GameObject questObjective;
    public GameObject questPosition;
    public GameObject player;

    public void Awake()
    {
        player = GameObject.Find("PlayerInstance");
        questName = GameObject.Find("QuestName");
        questObjective = GameObject.Find("QuestObjective");
        questPosition = GameObject.Find("QuestPosition");
    }

    public void init(int id)
    {
        NpcEventHandler.Instance._onTalkedToNpc += DisplayEvaluation;
        NpcEventHandler.Instance.afterTalkedToNpc += CheckGoals;
        NpcEventHandler.Instance.onTalkedToNpc += _DisplayEvaluation;
        CombatEventHandler.Instance.afterEnemyDeath += DisplayEvaluation;

        questGiverId = id;
    }

    public void CheckGoals()
    {
        if(goals.All(x => x.completed == true))
        {
            if (Active && !Completed)
            {
                CompleteQuest();
            }
        }
    }

    public void CompleteQuest()
    {
        GiveReward();
        Completed = true;
        Active = false;
        NpcEventHandler.Instance.afterTalkedToNpc -= CheckGoals;
        NpcEventHandler.Instance._onTalkedToNpc -= DisplayEvaluation;
        NpcEventHandler.Instance.onTalkedToNpc -= _DisplayEvaluation;
        CombatEventHandler.Instance.afterEnemyDeath -= DisplayEvaluation;
    }
    public void _DisplayEvaluation(int id, List<string> conversations, string name, List<Quest> quests)
    {
        DisplayEvaluation();
    }
    public void DisplayEvaluation()
    {
        if (Active)
        {
            SetQuestObjective("", true);
            SetQuestName(name);
            foreach (var goal in goals)
            {
                SetQuestObjective(goal.currentAmount + "/" + goal.requiredAmount, false);
                if(goal.currentAmount >= goal.requiredAmount)
                    SetQuestColor(Color.green);
            }
        }
    }

    public void GiveReward()
    {
        if (Experience != 0)
        {
            player.GetComponent<PlayerLevelController>().GetExperience(Experience);

        }
        if(spellReward != null)
        {
            UIEventHandler.Instance.DisplaySpellReward(spellReward);
        }
        if(itemReward != null)
        {
            //TODO item rewards
        }
    }

    public void ActivateQuest()
    {

        questName = (Instantiate(Resources.Load("QuestName"), GameObject.Find("QuestList").transform) as GameObject);
        questObjective = (Instantiate(Resources.Load("QuestObjective"), GameObject.Find("QuestList").transform) as GameObject);
        questName.transform.position = questPosition.transform.position;
        questObjective.transform.position = questPosition.transform.position;
        questObjective.transform.Translate(new Vector3(60, 0, 0));
        Active = true;
        DisplayEvaluation();
        questPosition.transform.Translate(new Vector3(0, -30, 0));
    }

    public void SetQuestName(string name)
    {
        questName.GetComponent<Text>().text = name;
    }

    public void SetQuestObjective(string objective, bool reset)
    {
        if (reset)
            questObjective.GetComponent<Text>().text = objective;
        else 
            questObjective.GetComponent<Text>().text += objective;
    }

    public void SetQuestColor(Color color)
    {
        questName.GetComponent<Text>().color = color;
        questObjective.GetComponent<Text>().color = color;
    }
}
