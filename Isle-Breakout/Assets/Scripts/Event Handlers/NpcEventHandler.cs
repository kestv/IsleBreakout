using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEventHandler : MonoBehaviour
{
    public delegate void OnTalkedToNpc(int id, List<string> conversations, string name, List<Quest> quests);
    public delegate void _OnTalkedToNpc();
    public delegate void AfterTalkedToNpc();

    public OnTalkedToNpc onTalkedToNpc;
    public _OnTalkedToNpc _onTalkedToNpc;
    public AfterTalkedToNpc afterTalkedToNpc;
    public static NpcEventHandler Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
