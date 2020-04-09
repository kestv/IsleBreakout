using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Terrain.activeTerrain.detailObjectDistance != 1000)
        {
            Terrain.activeTerrain.detailObjectDistance = 1000;
        }
    }
}
