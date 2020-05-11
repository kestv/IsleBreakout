using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField]int ID;
    [SerializeField]string _name;
    [SerializeField]List<string> conversations = new List<string>();
    [SerializeField]GameObject conversationText;
    [SerializeField]GameObject conversationButton;
    [SerializeField]GameObject nameTag;

    public List<Quest> quests;

    GameObject player;

    private void Start()
    {
        player = GameObject.Find("PlayerInstance");
        nameTag.GetComponent<TextMesh>().text = _name;
        conversationText = GameObject.Find("ConversationText");
        conversationButton = GameObject.Find("ConversationButton");
    }

    private void Update()
    {
        CheckForNameTag();
    }
    float GetDistance(Transform target)
    {
        return (Math.Abs(target.position.x - transform.position.x) + Math.Abs(target.position.z - transform.position.z));
    }
    void CheckForNameTag()
    {
        if(nameTag == null)
        {
            print("hoho");
        }
        if (GetDistance(player.transform) > 50 && nameTag.activeSelf == true)
        {
            nameTag.SetActive(false);
        }
        else if (GetDistance(player.transform) < 50 && nameTag.activeSelf == false)
        {
            nameTag.SetActive(true);
        }
    }

    public List<string> GetConversations()
    {
        return this.conversations;
    }

    public int GetId()
    {
        return this.ID;
    }

    public string GetName()
    {
        return this._name;
    }
}
