using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCameraRotate : MonoBehaviour
{
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0.1f, 0));
    }
}
