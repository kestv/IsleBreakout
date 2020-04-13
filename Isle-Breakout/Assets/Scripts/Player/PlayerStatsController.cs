using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsController : MonoBehaviour
{
    GameObject statsWindow;
    public float strength;
    public float speed;
    public float wisdom;
    public float remainingPoints;

    public GameObject strengthButton;
    public GameObject speedButton;
    public GameObject wisdomButton;

    public Text strengthValue;
    public Text speedValue;
    public Text wisdomValue;
    public Text remainingValue;
    

    // Start is called before the first frame update
    void Start()
    {
        strengthValue = GameObject.Find("StrengthValue").GetComponent<Text>();
        speedValue = GameObject.Find("SpeedValue").GetComponent<Text>();
        wisdomValue = GameObject.Find("WisdomValue").GetComponent<Text>();
        remainingValue = GameObject.Find("RemainingValue").GetComponent<Text>();

        strength = float.Parse(GameObject.Find("StrengthValue").GetComponent<Text>().text);
        speed = float.Parse(GameObject.Find("SpeedValue").GetComponent<Text>().text);
        wisdom = float.Parse(GameObject.Find("WisdomValue").GetComponent<Text>().text);

        strengthButton = GameObject.Find("StrengthButton");
        speedButton = GameObject.Find("SpeedButton");
        wisdomButton = GameObject.Find("WisdomButton");

        strengthButton.GetComponent<Button>().onClick.AddListener(delegate { improveStrength(1); });
        speedButton.GetComponent<Button>().onClick.AddListener(delegate { improveSpeed(1); });
        wisdomButton.GetComponent<Button>().onClick.AddListener(delegate { improveWisdom(1); });

        statsWindow = GameObject.Find("Stats");

        remainingPoints = GetComponent<PlayerLevelController>().level;
    }

    void improveStrength(float value)
    {
        if (remainingPoints > 0)
        {
            remainingPoints -= value;
            strength += value;
            strengthValue.text = strength.ToString();
        }
    }

    void improveSpeed(float value)
    {
        if (remainingPoints > 0)
        {
            remainingPoints -= value;
            speed += value;
            speedValue.text = speed.ToString();
            GetComponent<PlayerMovementController>().speed += speed/4;
        }
    }

    void improveWisdom(float value)
    {
        if (remainingPoints > 0)
        {
            remainingPoints -= value;
            wisdom += value;
            wisdomValue.text = wisdom.ToString();
        }
    }

    public void updateStrength(float value)
    {
        strength += value;
        //strengthValue.text = strength.ToString();
    }

    public void updateSpeed(float value)
    {
        speed += value;
        //speedValue.text = speed.ToString();
    }

    public void updateWisdom(float value)
    {
        wisdom += value;
        //wisdomValue.text = wisdom.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(statsWindow.activeSelf)
            {
                statsWindow.SetActive(false);
            }
            else
            {
                statsWindow.SetActive(true);
            }
            
        }
        remainingValue.text = remainingPoints.ToString();
    }
}
