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

    int conversationIndex;
    List<string> conversations;
    List<Quest> quests;

    int npcID;

    bool questAvailable = true;

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
        NpcMessagesHandler.Instance.onTalkedToNpc += StartConversation;
    }

    public void StartConversation(int ID, List<string> conversations, string name, List<Quest> quests)
    {
        questAvailable = true;
        npcID = ID;
        questAcceptButton.SetActive(false);
        if (quests != null && quests.Count > 0)
            for(int i = 0; i < quests.Count; i++)
            {
                var quest = quests[i];
                if (quest.IsCompleted() != true && quest.IsActive() != true)
                {
                    if (i > 0 && quests[i - 1].IsActive() == true && !quests[i - 1].Evaluate())
                    {
                        this.conversations = conversations;
                        questAvailable = false;
                        break;
                    }
                    this.conversations = quest.GetConversations();
                    break;
                }
                this.conversations = conversations;
            }
        else this.conversations = conversations;

        this.quests = quests;
        conversation.SetActive(true);
        this.name.GetComponent<Text>().text = name;
        conversationIndex = 1;
        if (this.conversations.Count == 0) this.conversations.Add("What do you want?");
        text.GetComponent<Text>().text = this.conversations[0];
        if (this.conversations.Count - conversationIndex >= 1)
        {
            buttonText.GetComponent<Text>().text = "Next";
        }
        else buttonText.GetComponent<Text>().text = "Close";
    }

    public void Iterate()
    {
        if (conversationIndex < conversations.Count - 1)
        {
            buttonText.GetComponent<Text>().text = "Next";
            text.GetComponent<Text>().text = conversations[conversationIndex];
            conversationIndex++;
        }
        else if (conversationIndex < conversations.Count)
        {
            buttonText.GetComponent<Text>().text = "Close";
            if (quests != null && quests.Count > 0 && quests.Find(q => q.IsCompleted() == false && q.IsActive() == false) && questAvailable)
                questAcceptButton.SetActive(true);
            text.GetComponent<Text>().text = conversations[conversationIndex];
            conversationIndex++;
        }
        else
        {
            conversation.SetActive(false);
            NpcMessagesHandler.Instance.afterTalkedToNpc(npcID);
        }
    }
    public void AcceptQuest()
    {
        NpcMessagesHandler.Instance.afterTalkedToNpc(npcID);
        foreach (var quest in quests)
        {
            if (quest.IsCompleted() == false && quest.IsActive() == false)
            {
                quest.ActivateQuest();
                conversation.SetActive(false);
                break;
            }
        }
    }
}
