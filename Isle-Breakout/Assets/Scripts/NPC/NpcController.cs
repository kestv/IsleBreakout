﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public int ID;
    public string _name;
    public List<string> conversations = new List<string>();
    public GameObject conversationText;
    public GameObject conversationButton;
    public GameObject nameTag;

    public List<Quest> quests;

    private void Start()
    {
        nameTag.GetComponent<TextMesh>().text = _name;
        conversationText = GameObject.Find("ConversationText");
        conversationButton = GameObject.Find("ConversationButton");
    }
}
