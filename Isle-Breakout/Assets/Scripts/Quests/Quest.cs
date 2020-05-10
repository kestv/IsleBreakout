using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    protected int questGiverId;
    protected List<Goal> goals = new List<Goal>();
    List<string> conversations = new List<string>();
    int experience;
    string questName;

    bool completed = false;
    bool active = false;

    [SerializeField]GameObject spellReward;
    [SerializeField]GameObject itemReward;
 
    [SerializeField]GameObject questNameLabel;
    [SerializeField]GameObject questObjective;
    [SerializeField]GameObject questPosition;
    [SerializeField]GameObject player;

    public void Awake()
    {
        player = GameObject.Find("PlayerInstance");
        questNameLabel = GameObject.Find("QuestName");
        questObjective = GameObject.Find("QuestObjective");
        questPosition = GameObject.Find("QuestPosition");
    }

    public void Init(int id)
    {
        NpcMessagesHandler.Instance._onTalkedToNpc += DisplayEvaluation;
        NpcMessagesHandler.Instance.afterTalkedToNpc += CheckGoals;
        NpcMessagesHandler.Instance.onTalkedToNpc += _DisplayEvaluation;
        CombatHandler.Instance.afterEnemyDeath += DisplayEvaluation;
        if(questGiverId == 0)
            questGiverId = id;
    }

    public void CheckGoals(int npcID)
    {
        if(goals.All(x => x.IsCompleted() == true))
        {
            if (active && !completed && questGiverId == npcID)
            {
                if (itemReward != null && player.GetComponent<PlayerInventory>().inventoryFull)
                {
                    UIHandler.Instance.DisplayMessage("Inventory is full");
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
        completed = true;
        active = false;
        NpcMessagesHandler.Instance.afterTalkedToNpc -= CheckGoals;
        NpcMessagesHandler.Instance._onTalkedToNpc -= DisplayEvaluation;
        NpcMessagesHandler.Instance.onTalkedToNpc -= _DisplayEvaluation;
        CombatHandler.Instance.afterEnemyDeath -= DisplayEvaluation;
    }
    public void _DisplayEvaluation(int id, List<string> conversations, string name, List<Quest> quests)
    {
        DisplayEvaluation();
    }
    public void DisplayEvaluation()
    {
        if (active)
        {
            SetQuestObjective("", true);
            SetQuestName(questName);
            foreach (var goal in goals)
            {
                SetQuestObjective(goal.GetCurrentAmount() + "/" + goal.GetRequiredAmount(), false);
                if(goal.GetCurrentAmount() >= goal.GetRequiredAmount())
                    SetQuestColor(Color.green);
            }
        }
    }

    public bool Evaluate()
    {
        foreach (var goal in goals)
        {
            if (goal.GetCurrentAmount() >= goal.GetRequiredAmount())
                return true;
        }
        return false;
    }

    public void GiveReward()
    {
        if (experience != 0)
        {
            player.GetComponent<PlayerLevelController>().GetExperience(experience);
        }
        if(spellReward != null)
        {
            UIHandler.Instance.DisplaySpellReward(spellReward);
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
        questNameLabel = (Instantiate(Resources.Load("QuestName"), GameObject.Find("QuestList").transform) as GameObject);
        questObjective = (Instantiate(Resources.Load("QuestObjective"), GameObject.Find("QuestList").transform) as GameObject);
        questNameLabel.transform.position = questPosition.transform.position;
        questObjective.transform.position = questPosition.transform.position;
        questObjective.transform.Translate(new Vector3(150, 0, 0));
        active = true;
        DisplayEvaluation();
        questPosition.transform.Translate(new Vector3(0, -30, 0));
    }

    public void SetQuestName(string name)
    {
        questNameLabel.GetComponent<Text>().text = name;
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
        questNameLabel.GetComponent<Text>().color = color;
        questObjective.GetComponent<Text>().color = color;
    }

    public void RemoveQuest()
    {
        questPosition.transform.position = questNameLabel.transform.position;
        Destroy(questNameLabel);
        Destroy(questObjective);
    }

    public bool IsCompleted()
    {
        return this.completed;
    }

    public bool IsActive()
    {
        return this.active;
    }

    public List<string> GetConversations()
    {
        return this.conversations;
    }
}
