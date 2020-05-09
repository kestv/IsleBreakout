using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelController : MonoBehaviour
{
    float levelRate;
    float level;
    float currentExperiencePoints;
    float requiredExperiencePoints;
    float totalXp;
    float currentGameXp;

    GameObject levelField;

    public void Start()
    {
        levelField = GameObject.Find("Level");
        level = Player.level;
        totalXp = Player.xp;
        currentGameXp = 0;
        levelField.GetComponent<Text>().text = level.ToString();
        CombatEventHandler.Instance.onEnemyDeath += GetExperience;
    }

    public void GetExperience(float amount)
    {
        totalXp += amount;
        currentGameXp += amount;
        currentExperiencePoints += amount;
        UIEventHandler.Instance.DisplayReward(amount + " xp", false);
        EvaluateXp();
    }
    //For events
    public void GetExperience(float amount, int id)
    {
        totalXp += amount;
        currentGameXp += amount;
        currentExperiencePoints += amount;
        UIEventHandler.Instance.DisplayReward(amount + " xp", false);
        EvaluateXp();
    }

    public void EvaluateXp()
    {
        while (currentExperiencePoints >= requiredExperiencePoints)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        UIEventHandler.Instance.DisplayReward("Level up!", true);
        level += 1;
        currentExperiencePoints -= requiredExperiencePoints;
        requiredExperiencePoints *= levelRate;

        //TODO bad position
        var hpCanvas = GetComponent<PlayerHealthController>().GetHealthbarCanvas();
        levelField.GetComponent<Text>().text = level.ToString();
        GetComponent<PlayerStatsController>().IncreaseRemainingPoints(1);
        GetComponent<PlayerStatsController>().UpdateRemainingPointsValue();

        UIEventHandler.Instance.DisplayReward("Level up!", true);
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
