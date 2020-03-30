using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEventHandler : MonoBehaviour
{
    public delegate void OnEnemyDeath(float xp, int id);
    public OnEnemyDeath onEnemyDeath;
    public delegate void AfterEnemyDeath();
    public AfterEnemyDeath afterEnemyDeath;
    public static CombatEventHandler Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
