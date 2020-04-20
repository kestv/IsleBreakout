using System;
using System.Collections;
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
    float getDistance(Transform target)
    {
        return (Math.Abs(target.position.x - transform.position.x) + Math.Abs(target.position.z - transform.position.z));
    }
    void CheckForNameTag()
    {
        if (getDistance(player.transform) > 50 && nameTag.activeSelf == true)
        {
            nameTag.SetActive(false);
        }
        else if (getDistance(player.transform) < 50 && nameTag.activeSelf == false)
        {
            nameTag.SetActive(true);
        }
    }
}
