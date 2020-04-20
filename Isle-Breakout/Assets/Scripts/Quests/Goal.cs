using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public bool completed;
    public int requiredAmount;
    public int currentAmount;

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
}
