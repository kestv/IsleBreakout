﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCameraRotate : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0.1f, 0));
    }
}
