using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public KillGoal(string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.description = description;
        this.completed = completed;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
    }

    public override void Init()
    {
        CombatEventHandler.Instance.onEnemyDeath += EnemyDied;
        base.Init();
    }

    public void EnemyDied(float xp)
    {
        this.currentAmount ++;
        Evaluate();
    }
}
