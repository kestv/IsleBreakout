using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Helmet : ScriptableObject, IArmor
{
    [Header("Helmet meshes")]
    [SerializeField]
    public Mesh helmet;

    [Header("Stats")]
    [SerializeField]
    public float strength;
    [SerializeField]
    public float wisdom;
    [SerializeField]
    public float speed;
    [SerializeField]
    public float hp;

    public List<Mesh> getMeshes()
    {
        List<Mesh> meshes = new List<Mesh>();
        meshes.Add(helmet);
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
        return "Helmet";
    }
}
