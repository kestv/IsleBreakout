using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button button;
    public GameObject window;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerInstance");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void closeWindow()
    {
        window.SetActive(false);
        //player.GetComponent<QuestController>().StartQuest();
    }
}
