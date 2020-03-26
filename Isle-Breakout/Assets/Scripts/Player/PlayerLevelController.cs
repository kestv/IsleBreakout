using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelController : MonoBehaviour
{
    public float levelRate;
    public int level;
    public float currentExperiencePoints;
    public float requiredExperiencePoints;

    public void Start()
    {
        //CombatEvents.getInstance().onEnemyDeathDelegate += GetExperience;
    }
    public void GetExperience(float amount)
    {
        currentExperiencePoints += amount;
        while(currentExperiencePoints >= requiredExperiencePoints)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level += 1;
        currentExperiencePoints -= requiredExperiencePoints;
        requiredExperiencePoints *= levelRate;

        //TODO bad position
        var hpCanvas = GetComponent<PlayerHealthController>().getHealthbarCanvas();
        GetComponent<PlayerCombatController>().levelField.GetComponent<Text>().text = level.ToString();
        GetComponent<PlayerStatsController>().remainingPoints++;
    }
}
