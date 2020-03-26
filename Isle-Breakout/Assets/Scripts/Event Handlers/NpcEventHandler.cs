using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEventHandler : MonoBehaviour
{
    public delegate void OnTalkedToNpc(int id);
    public OnTalkedToNpc onTalkedToNpc;
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
