using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameRotationController : MonoBehaviour
{
    int fontSize;
    private void Start()
    {
        fontSize = GetComponent<TextMesh>().fontSize;
    }
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }


}
