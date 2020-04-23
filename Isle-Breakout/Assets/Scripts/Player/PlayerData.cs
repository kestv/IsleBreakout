using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public string name;
    public float level;
    public float xp;
    public int id;
    public int totalGamesPlayed;
    public float totalTimeSpent;

    public PlayerData(string name, float level, float xp, int id, int games, float time)
    {
        this.name = name;
        this.level = level;
        this.xp = xp;
        this.id = id;
        this.totalGamesPlayed = games;
        this.totalTimeSpent = time;
    }
    
    public PlayerData()
    {

    }
}
