using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelController : MonoBehaviour
{
    public float levelRate;
    public float level;
    public float currentExperiencePoints;
    public float requiredExperiencePoints;
    public float totalXp;

    public void Start()
    {
        level = Player.level;
        totalXp = Player.xp;
        GetComponent<PlayerCombatController>().levelField.GetComponent<Text>().text = level.ToString();
        CombatEventHandler.Instance.onEnemyDeath += GetExperience;
    }

    public void GetExperience(float amount)
    {
        totalXp += amount;
        currentExperiencePoints += amount;
        UIEventHandler.Instance.DisplayReward(amount + " xp", false);
        EvaluateXp();
    }
    //For events
    public void GetExperience(float amount, int id)
    {
        totalXp += amount;
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
        var hpCanvas = GetComponent<PlayerHealthController>().getHealthbarCanvas();
        GetComponent<PlayerCombatController>().levelField.GetComponent<Text>().text = level.ToString();
        GetComponent<PlayerStatsController>().remainingPoints++;

        UIEventHandler.Instance.DisplayReward("Level up!", true);
    }
}
