using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    int enemyId;
    public KillGoal(int requiredAmount, int enemyId)
    {
        this.completed = false;
        this.currentAmount = 0;
        this.requiredAmount = requiredAmount;
        this.enemyId = enemyId;
    }

    public override void Init()
    {
        CombatHandler.Instance.onEnemyDeath += EnemyDied;
        currentAmount = 0;
        base.Init();
    }

    public void EnemyDied(float xp, int id)
    {
        Debug.Log(enemyId + " " + id);
        if (enemyId >= 0)
        {
            if (enemyId == id)
            {
                if (currentAmount < requiredAmount)
                    this.currentAmount++;
                Evaluate();
            }
        }
        else
        {
            if (currentAmount < requiredAmount)
                this.currentAmount++;
            Evaluate();
        }
    }

}
