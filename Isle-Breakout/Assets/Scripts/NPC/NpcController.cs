using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public int ID;
    public string name;
    public List<string> conversations = new List<string>();
    public GameObject conversationText;
    public GameObject conversationButton;

    public List<Quest> quests;
}
