using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNoise : MonoBehaviour
{
    public float power = 3f;
    public float scale = 1f;
    public float timeScale = 1f;

    float offsetX;
    float offsetY;
    MeshFilter meshFilter;
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        MakeNoise();
    }

    
    void Update()
    {
        MakeNoise();
        offsetX += Time.deltaTime * timeScale;
        offsetY += Time.deltaTime * timeScale;
    }

    void MakeNoise()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = CalculateHeight(vertices[i].x, vertices[i].z) * power;
        }

        meshFilter.mesh.vertices = vertices;
    }

    float CalculateHeight(float x, float y)
    {
        float xCord = x * scale + offsetX;
        float yCord = y * scale + offsetY;

        return Mathf.PerlinNoise(xCord, yCord);
    }
}
