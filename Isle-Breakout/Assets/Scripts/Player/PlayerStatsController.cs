using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsController : MonoBehaviour
{
    GameObject statsWindow;
    float hp;
    float strength;
    float speed;
    float wisdom;
    float remainingPoints;
    bool firstTimeOpen;

    [SerializeField] private GameObject strengthButton;
    [SerializeField] private GameObject speedButton;
    [SerializeField] private GameObject wisdomButton;

    [SerializeField] TextMeshProUGUI strengthValue;
    [SerializeField] TextMeshProUGUI speedValue;
    [SerializeField] TextMeshProUGUI wisdomValue;
    [SerializeField] TextMeshProUGUI remainingValue;

    PlayerHealthController healthCtrl;

    // Start is called before the first frame update
    void Start()
    {
        DependencyManager manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        Transform canvas = manager.getCanvas().transform;
        CanvasController ctrl = manager.getCanvasController();
        Transform equipPanel = ctrl.getEquipPanel();
        strengthButton = equipPanel.GetChild(0).GetChild(4).gameObject;
        speedButton = equipPanel.GetChild(0).GetChild(5).gameObject;
        wisdomButton = equipPanel.GetChild(0).GetChild(6).gameObject;

        strengthValue   = equipPanel.GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        speedValue      = equipPanel.GetChild(0).GetChild(3).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        wisdomValue     = equipPanel.GetChild(0).GetChild(3).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        remainingValue  = equipPanel.GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>();

        strength = float.Parse(GameObject.Find("StrengthValue").GetComponent<Text>().text);
        speed = float.Parse(GameObject.Find("SpeedValue").GetComponent<Text>().text);
        wisdom = float.Parse(GameObject.Find("WisdomValue").GetComponent<Text>().text);

        strengthButton.GetComponent<Button>().onClick.AddListener(delegate { ImproveStrength(1); });
        speedButton.GetComponent<Button>().onClick.AddListener(delegate { ImproveSpeed(1); });
        wisdomButton.GetComponent<Button>().onClick.AddListener(delegate { ImproveWisdom(1); });

        statsWindow = GameObject.Find("Stats");

        remainingPoints = GetComponent<PlayerLevelController>().GetLevel();
        healthCtrl = GetComponent<PlayerHealthController>();
        statsWindow.SetActive(false);
        UpdateRemainingPointsValue();
        firstTimeOpen = true;
    }

    public void ImproveStrength(float value)
    {
        if (remainingPoints > 0)
        {
            remainingPoints -= value;
            strength += value;
            strengthValue.text = strength.ToString();
            UpdateRemainingPointsValue();
        }
    }

    public void ImproveSpeed(float value)
    {
        if (remainingPoints > 0)
        {
            remainingPoints -= value;
            speed += value;
            speedValue.text = speed.ToString();
            GetComponent<PlayerMovementController>().AddSpeed(speed);
            UpdateRemainingPointsValue();
        }
    }

    public void ImproveWisdom(float value)
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
        UpdateStrength(strength);
        strengthValue.text = strength.ToString();
        UpdateSpeed(speed);
        speedValue.text = speed.ToString();
        UpdateWisdom(wisdom);
        wisdomValue.text = wisdom.ToString();
    }

    public void Open()
    { 
        if(firstTimeOpen)
        {
            remainingPoints = GetComponent<PlayerLevelController>().GetLevel();
            firstTimeOpen = false;
        }
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

    public void UpdateStrength(float value)
    {
        strength += value;
    }

    public void UpdateSpeed(float value)
    {
        speed += value;
        GetComponent<PlayerMovementController>().AddSpeed(value);
    }

    public void UpdateWisdom(float value)
    {
        wisdom += value;
    }

    public void UpdateHP(float value)
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

    public float GetStrength()
    {
        return this.strength;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    public float GetWisdom()
    {
        return this.wisdom;
    }

    public void IncreaseRemainingPoints(float points)
    {
        this.remainingPoints += points;
    }

    //TESTS
    public float GetRemainingPoints()
    {
        return this.remainingPoints;
    }


}
