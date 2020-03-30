using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventHandler : MonoBehaviour
{
    public delegate void OnRewardReceive();
    public OnRewardReceive onRewardReceive;
    public GameObject rewardObject;
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
        rewardObject = GameObject.Find("Reward");
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
}
