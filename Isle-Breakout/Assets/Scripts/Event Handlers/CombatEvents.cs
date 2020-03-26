using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvents : MonoBehaviour
{
    //Total improvisation
    public delegate void OnEnemyDeath();
    public OnEnemyDeath onEnemyDeathDelegate;
    static CombatEvents combatEvents;

    public static CombatEvents getInstance()
    {
        if (!combatEvents) return combatEvents = new CombatEvents();
        else
        {
            return combatEvents;
        }
    }
}
