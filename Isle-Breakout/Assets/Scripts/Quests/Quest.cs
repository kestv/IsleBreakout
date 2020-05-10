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
    public string QuestName;

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
        if(questGiverId == 0)
            questGiverId = id;
    }

    public void CheckGoals(int npcID)
    {
        if(goals.All(x => x.completed == true))
        {
            if (Active && !Completed && questGiverId == npcID)
            {
                if (itemReward != null && player.GetComponent<PlayerInventory>().getInventoryFull())
                {
                    UIEventHandler.Instance.DisplayMessage("Inventory is full");
                }
                else
                {
                    Debug.Log("Inside complete");
                    CompleteQuest();
                } 
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
            SetQuestName(QuestName);
            foreach (var goal in goals)
            {
                SetQuestObjective(goal.currentAmount + "/" + goal.requiredAmount, false);
                if(goal.currentAmount >= goal.requiredAmount)
                    SetQuestColor(Color.green);
            }
        }
    }

    public bool Evaluate()
    {
        foreach (var goal in goals)
        {
            if (goal.currentAmount >= goal.requiredAmount)
                return true;
        }
        return false;
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
            var inv = player.GetComponent<PlayerInventory>();
            inv.AddItem(Instantiate(itemReward));
        }
        RemoveQuest();
    }

    public void ActivateQuest()
    {
        goals.ForEach(x => x.Init());
        questName = (Instantiate(Resources.Load("QuestName"), GameObject.Find("QuestList").transform) as GameObject);
        questObjective = (Instantiate(Resources.Load("QuestObjective"), GameObject.Find("QuestList").transform) as GameObject);
        questName.transform.position = questPosition.transform.position;
        questObjective.transform.position = questPosition.transform.position;
        questObjective.transform.Translate(new Vector3(150, 0, 0));
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

    public void RemoveQuest()
    {
        questPosition.transform.position = questName.transform.position;
        Destroy(questName);
        Destroy(questObjective);
    }
}
