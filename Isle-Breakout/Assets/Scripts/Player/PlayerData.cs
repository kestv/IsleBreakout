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

    public PlayerData(string name, float level, float xp, int id)
    {
        this.name = name;
        this.level = level;
        this.xp = xp;
        this.id = id;
    }
    
    public PlayerData()
    {

    }
}
