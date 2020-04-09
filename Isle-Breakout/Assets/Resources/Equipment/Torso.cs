using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Torso : ScriptableObject, IArmor
{
    [Header("Torso meshes")]
    [SerializeField]
    public Mesh torso;
    [SerializeField]
    public Mesh armUpperRight;
    [SerializeField]
    public Mesh armUpperLeft;
    [SerializeField]
    public Mesh armLowerRight;
    [SerializeField]
    public Mesh armLowerLeft;
    [SerializeField]
    public Mesh handRight;
    [SerializeField]
    public Mesh handLeft;

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
        meshes.Add(torso);
        meshes.Add(armUpperRight);
        meshes.Add(armUpperLeft);
        meshes.Add(armLowerRight);
        meshes.Add(armLowerLeft);
        meshes.Add(handRight);
        meshes.Add(handLeft);
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
}
