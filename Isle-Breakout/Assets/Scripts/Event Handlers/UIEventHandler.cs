using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventHandler : MonoBehaviour
{
    public delegate void OnRewardReceive();
    public OnRewardReceive onRewardReceive;
    public GameObject rewardObject;
    public GameObject rewardUI;
    public GameObject infoMessage;
    float lastMessage;
    public static UIEventHandler Instance { get; private set; }

    private void Awake()
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
    }
    private void Start()
    {
        rewardObject.SetActive(false);
        rewardUI.SetActive(false);
    }
    public void DisplayReward(string reward, bool levelUp)
    {
        rewardObject.SetActive(true);
        if (levelUp)
            rewardObject.GetComponent<Text>().color = Color.yellow;
        else rewardObject.GetComponent<Text>().color = Color.green;
        rewardObject.GetComponent<Text>().text = levelUp ? reward : "+ " + reward;
        rewardObject.GetComponent<ScrollController>().StartMoving();
    }
    
    public void DisplaySpellReward(GameObject spell)
    {
        rewardUI.SetActive(true);
        rewardUI.GetComponent<SpellRewardController>().SetSpell(spell);
    }

    public void DisplayMessage(string message)
    {
        if (Time.time - lastMessage > 1)
        {
            infoMessage.SetActive(true);
            infoMessage.GetComponent<Text>().text = message;
            infoMessage.GetComponent<ScrollController>().StartMoving();
            lastMessage = Time.time;
        }
    }
}
