using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    float playTime;
    void Start()
    {
        playTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetTime()
    {
        var time = Time.time - playTime;
        return time;
    }
}
