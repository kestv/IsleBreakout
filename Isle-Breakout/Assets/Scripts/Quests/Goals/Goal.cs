using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    protected bool completed;
    protected int requiredAmount;
    protected int currentAmount;

    public virtual void Init()
    {

    }

    public void Complete()
    {
        completed = true;
    }

    public void Evaluate()
    {
        if(currentAmount >= requiredAmount)
        {
            Complete();
        }
    }

    public int GetRequiredAmount()
    {
        return this.requiredAmount;
    }

    public int GetCurrentAmount()
    {
        return this.currentAmount;
    }

    public bool IsCompleted()
    {
        return this.completed;
    }
}
