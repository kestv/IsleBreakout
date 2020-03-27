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
    ConversationHandler Instance;

    int i;
    List<string> conversations;
    List<Quest> quests;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        conversation = GameObject.Find("Conversation");
        text = GameObject.Find("ConversationText");
        button = GameObject.Find("ConversationButton");
        buttonText = GameObject.Find("ConversationButtonText");
        name = GameObject.Find("ConversationName");
        questAcceptButton = GameObject.Find("ConversationQuestAcceptButton");
    }

    void Start()
    {       
        //conversation.SetActive(false);
        NpcEventHandler.Instance.onTalkedToNpc += StartConversation;
    }

    public void StartConversation(int ID, List<string> conversations, string name, List<Quest> quests)
    {
        Debug.Log("StartConversation");
        conversation.SetActive(true);
        questAcceptButton.SetActive(false);

        this.conversations = conversations;
        this.quests = quests;

        this.name.GetComponent<Text>().text = name;
        i = 1;
        text.GetComponent<Text>().text = conversations[0];
        if (conversations.Count - i > 1)
        {
            buttonText.GetComponent<Text>().text = "Next";
        }
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
            if(quests.Count > 0 && quests.Find(q => q.Completed == false && q.Active == false))
                questAcceptButton.SetActive(true);
            text.GetComponent<Text>().text = conversations[i];
            i++;
        }
        else conversation.SetActive(false);
    }

    public void AcceptQuest()
    {
        foreach (var quest in quests)
        {
            if(quest.Completed == false)
            {
                quest.Active = true;
                conversation.SetActive(false);
                break;
            }
        }
    }
}
