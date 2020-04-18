using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsController : MonoBehaviour
{
    GameObject statsWindow;
    public float hp;
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

    PlayerHealthController healthCtrl;

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
        healthCtrl = GetComponent<PlayerHealthController>();
        statsWindow.SetActive(false);
    }

    public void improveStrength(float value)
    {
        if (remainingPoints > 0)
        {
            remainingPoints -= value;
            strength += value;
            strengthValue.text = strength.ToString();
            remainingValue.text = remainingPoints.ToString();
        }
    }

    public void improveSpeed(float value)
    {
        if (remainingPoints > 0)
        {
            remainingPoints -= value;
            speed += value;
            speedValue.text = speed.ToString();
            GetComponent<PlayerMovementController>().speed += speed/4;
            remainingValue.text = remainingPoints.ToString();
        }
    }

    public void improveWisdom(float value)
    {
        if (remainingPoints > 0)
        {
            remainingPoints -= value;
            wisdom += value;
            wisdomValue.text = wisdom.ToString();
            remainingValue.text = remainingPoints.ToString();
        }
    }

    public void AddBonuses(float strength, float speed, float wisdom)
    {
        this.strength += strength;
        strengthValue.text = strength.ToString();
        this.speed += speed;
        speedValue.text = speed.ToString();
        this.wisdom += wisdom;
        wisdomValue.text = wisdom.ToString();
    }

    public void Open()
    { 
        if(statsWindow.activeSelf)
        {
            statsWindow.SetActive(false);
        }
        else
        {
            remainingValue.text = remainingPoints.ToString();
            statsWindow.SetActive(true);
        }        
    }

    public void updateStrength(float value)
    {
        strength += value;
    }

    public void updateSpeed(float value)
    {
        speed += value;
    }

    public void updateWisdom(float value)
    {
        wisdom += value;
    }

    public void updateHP(float value)
    {
        hp += value;
    }

    public void IncreaseMaxHealth(float value)
    {
        healthCtrl.IncreaseMaxHealth(value);
    }

    public void Heal(float value)
    {
        healthCtrl.Heal(value);
    }

    public void Eat(float value)
    {
        healthCtrl.Eat(value);
    }
}
