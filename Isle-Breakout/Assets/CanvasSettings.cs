using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSettings : MonoBehaviour
{
    public GameObject targetPlayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getTargetPlayer()
    { return targetPlayer; }

    public void setTargetPlayer(GameObject targetPlayer)
    { this.targetPlayer = targetPlayer; }
}
