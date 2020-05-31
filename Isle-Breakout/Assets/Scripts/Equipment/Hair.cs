using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Hair : ScriptableObject, IArmor
{
    [Header("Hair mesh")]
    [SerializeField]
    private Mesh hair;

    [Header("Stats")]
    [SerializeField]
    private float strength;
    [SerializeField]
    private float wisdom;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float hp;

    public List<Mesh> getMeshes()
    {
        List<Mesh> meshes = new List<Mesh>();
        meshes.Add(hair);
        return meshes;
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getStrength()
    {
        return strength;
    }

    public float getWisdom()
    {
        return wisdom;
    }

    public float getHP()
    {
        return hp;
    }

    public string getType()
    {
        return "Hair";
    }
}
