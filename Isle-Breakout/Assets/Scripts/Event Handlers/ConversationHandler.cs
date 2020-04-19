using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ConversationHandler : MonoBehaviour
{
    GameObject conversation;
    GameObject text;
    GameObject button;
    GameObject buttonText;
    GameObject name;
    GameObject questAcceptButton;
    Text questName;
    Text questObjective;

    int i;
    List<string> conversations;
    List<Quest> quests;

    int npcID;

    public void Awake()
    {
        conversation = GameObject.Find("Conversation");
        text = GameObject.Find("ConversationText");
        button = GameObject.Find("ConversationButton");
        buttonText = GameObject.Find("ConversationButtonText");
        name = GameObject.Find("ConversationName");
        questAcceptButton = GameObject.Find("ConversationQuestAcceptButton");
    }

    void Start()
    {
        List<string> startConv = new List<string>();
        startConv.Add("Woah, what happened?");
        startConv.Add("I must have crashed..");
        startConv.Add("But why?");
        startConv.Add("Seems like I've been here already..");
        startConv.Add("I might need a ship to get away from here");
        StartConversation(-1, startConv, "", null);
        NpcEventHandler.Instance.onTalkedToNpc += StartConversation;
    }

    public void StartConversation(int ID, List<string> conversations, string name, List<Quest> quests)
    {
        npcID = ID;
        questAcceptButton.SetActive(false);
        if (quests != null && quests.Count > 0)
            foreach (var quest in quests)
            {
                if (quest.Completed != true && quest.Active != true)
                {
                    this.conversations = quest.conversations;
                    break;
                }
                this.conversations = conversations;
            }
        else this.conversations = conversations;

        this.quests = quests;
        conversation.SetActive(true);
        this.name.GetComponent<Text>().text = name;
        i = 1;
        text.GetComponent<Text>().text = this.conversations[0];
        if (this.conversations.Count - i >= 1)
        {
            buttonText.GetComponent<Text>().text = "Next";
        }
        else buttonText.GetComponent<Text>().text = "Close";
    }

    public void Iterate()
    {
        if (i < conversations.Count - 1)
        {
            buttonText.GetComponent<Text>().text = "Next";
            text.GetComponent<Text>().text = conversations[i];
            i++;
        }
        else if (i < conversations.Count)
        {
            buttonText.GetComponent<Text>().text = "Close";
            if (quests != null && quests.Count > 0 && quests.Find(q => q.Completed == false && q.Active == false))
                questAcceptButton.SetActive(true);
            text.GetComponent<Text>().text = conversations[i];
            i++;
        }
        else
        {
            conversation.SetActive(false);
            NpcEventHandler.Instance.afterTalkedToNpc(npcID);
        }
    }
    public void AcceptQuest()
    {
        foreach (var quest in quests)
        {
            if (quest.Completed == false && quest.Active == false)
            {
                quest.ActivateQuest();
                conversation.SetActive(false);
                break;
            }
        }
    }
}
