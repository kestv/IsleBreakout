using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject nameTag;
    public float xp;
    public int id;
    public string name;

    public void Awake()
    {
        nameTag.GetComponent<TextMesh>().text = name;
    }
}
