using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public delegate void OnEnemyDeath(float xp, int id);
    public OnEnemyDeath onEnemyDeath;
    public delegate void AfterEnemyDeath();
    public AfterEnemyDeath afterEnemyDeath;
    public static CombatHandler Instance { get; private set; }
    public void Awake()
    {
        GetInstance(); 
    }

    void GetInstance()
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
