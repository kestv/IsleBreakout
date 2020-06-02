using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelController : MonoBehaviour
{
    [SerializeField]float levelRate;
    [SerializeField]float level;
    [SerializeField]float currentExperiencePoints;
    [SerializeField]float requiredExperiencePoints;
    [SerializeField]float totalXp;
    [SerializeField]float currentGameXp;

    GameObject levelField;
    GameObject xpBar;

    private void OnLevelWasLoaded(int level)
    {
        //this.level = Player.level;
        //totalXp = Player.xp;
        //currentExperiencePoints = Player.xp;
        //levelField.GetComponent<Text>().text = level.ToString();
        //EvaluateXp(false);
        //DisplayXp(0);
        totalXp = Player.xp;
        currentExperiencePoints = totalXp;
        EvaluateXp(false);
    }
    public void Start()
    {
        levelField = GameObject.Find("Level");
        xpBar = GameObject.Find("Xp");
        currentGameXp = 0;
        levelField.GetComponent<Text>().text = level.ToString();
        CombatHandler.Instance.onEnemyDeath += GetExperience;
        DisplayXp(0);
    }

    public void GetExperience(float amount)
    {
        totalXp += amount;
        currentGameXp += amount;
        currentExperiencePoints += amount;
        DisplayXp(amount);
        UIHandler.Instance.DisplayReward(amount + " xp", false);
        EvaluateXp();
    }
    //For events
    public void GetExperience(float amount, int id)
    {
        totalXp += amount;
        currentGameXp += amount;
        currentExperiencePoints += amount;
        DisplayXp(amount);
        EvaluateXp();
    }

    public void EvaluateXp(bool levelUp = true)
    {
        while (currentExperiencePoints >= requiredExperiencePoints)
        {
            LevelUp(levelUp);
        }
    }

    public void LevelUp(bool levelUp = true)
    {
        if(levelUp)
            level += 1;
        currentExperiencePoints -= requiredExperiencePoints;
        requiredExperiencePoints *= levelRate;
        requiredExperiencePoints = Mathf.Floor(requiredExperiencePoints);
        
        //TODO bad position
        var hpCanvas = GetComponent<PlayerHealthController>().GetHealthbarCanvas();
        levelField.GetComponent<Text>().text = level.ToString();
        GetComponent<PlayerStatsController>().IncreaseRemainingPoints(1);
        GetComponent<PlayerStatsController>().UpdateRemainingPointsValue();
        DisplayXp(0);
        UIHandler.Instance.DisplayReward("Level up!", true);
    }

    void DisplayXp(float amount)
    {
        xpBar.GetComponent<Image>().fillAmount = currentExperiencePoints / requiredExperiencePoints;
        xpBar.transform.GetChild(1).GetComponent<Text>().text = string.Format("{0}/{1}", currentExperiencePoints, requiredExperiencePoints);
    }

    public float GetLevel()
    {
        return this.level;
    }

    public float GetTotalXp()
    {
        return this.totalXp;
    }

    public float GetCurrentGameXp()
    {
        return this.currentGameXp;
    }
}
