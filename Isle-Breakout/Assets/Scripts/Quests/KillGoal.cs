using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    int enemyId;
    public KillGoal(string description, int requiredAmount, int enemyId)
    {
        this.description = description;
        this.completed = false;
        this.currentAmount = 0;
        this.requiredAmount = requiredAmount;
        this.enemyId = enemyId;
    }

    public override void Init()
    {
        CombatEventHandler.Instance.onEnemyDeath += EnemyDied;
        base.Init();
    }

    public void EnemyDied(float xp, int id)
    {
        if (enemyId == id)
        {
            this.currentAmount++;
            Evaluate();
        }
    }

}
