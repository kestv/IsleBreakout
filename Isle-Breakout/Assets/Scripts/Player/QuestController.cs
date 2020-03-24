using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public List<GameObject> quests;
    public GameObject questWindow;
    public GameObject questView;
    void Start()
    {
        
    }

    public void StartQuest()
    {
        Instantiate(questView, questWindow.transform);
    }

    void Update()
    {
        
    }
}
