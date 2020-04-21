using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsController : MonoBehaviour
{
    GameObject statsWindow;
    public float hp;
    public float strength;
    public float speed;
    public float wisdom;
    public float remainingPoints;

    [SerializeField] private GameObject strengthButton;
    [SerializeField] private GameObject speedButton;
    [SerializeField] private GameObject wisdomButton;

    public TextMeshProUGUI strengthValue;
    public TextMeshProUGUI speedValue;
    public TextMeshProUGUI wisdomValue;
    public TextMeshProUGUI remainingValue;

    PlayerHealthController healthCtrl;

    // Start is called before the first frame update
    void Start()
    {
        DependencyManager manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        Transform canvas = manager.getCanvas().transform;
        strengthButton = canvas.GetChild(3).GetChild(0).GetChild(4).gameObject;
        speedButton = canvas.GetChild(3).GetChild(0).GetChild(5).gameObject;
        wisdomButton = canvas.GetChild(3).GetChild(0).GetChild(6).gameObject;

        strengthValue   = canvas.GetChild(3).GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        speedValue      = canvas.GetChild(3).GetChild(0).GetChild(3).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        wisdomValue     = canvas.GetChild(3).GetChild(0).GetChild(3).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        remainingValue  = canvas.GetChild(3).GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>();

        strength = float.Parse(GameObject.Find("StrengthValue").GetComponent<Text>().text);
        speed = float.Parse(GameObject.Find("SpeedValue").GetComponent<Text>().text);
        wisdom = float.Parse(GameObject.Find("WisdomValue").GetComponent<Text>().text);

        strengthButton.GetComponent<Button>().onClick.AddListener(delegate { improveStrength(1); });
        speedButton.GetComponent<Button>().onClick.AddListener(delegate { improveSpeed(1); });
        wisdomButton.GetComponent<Button>().onClick.AddListener(delegate { improveWisdom(1); });

        statsWindow = GameObject.Find("Stats");

        remainingPoints = GetComponent<PlayerLevelController>().level;
        healthCtrl = GetComponent<PlayerHealthController>();
        statsWindow.SetActive(false);
        UpdateRemainingPointsValue();
    }

    public void improveStrength(float value)
    {
        if (remainingPoints > 0)
        {
            remainingPoints -= value;
            strength += value;
            strengthValue.text = strength.ToString();
            UpdateRemainingPointsValue();
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
            UpdateRemainingPointsValue();
        }
    }

    public void improveWisdom(float value)
    {
        if (remainingPoints > 0)
        {
            remainingPoints -= value;
            wisdom += value;
            wisdomValue.text = wisdom.ToString();
            UpdateRemainingPointsValue();
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
        GetComponent<PlayerMovementController>().speed += speed / 4;
    }

    public void updateWisdom(float value)
    {
        wisdom += value;
    }

    public void updateHP(float value)
    {
        hp += value;
        IncreaseMaxHealth(value);
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

    public void UpdateRemainingPointsValue()
    {
        remainingValue.text = "Remaining skill points - " + remainingPoints.ToString();
    }
}
