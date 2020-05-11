using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject nameTag;
    public float xp;
    public int id;
    public string _name;

    public GameObject GetNameTag()
    {
        return this.nameTag;
    }
}
