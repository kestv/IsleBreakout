﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    public GameObject polymorphModel;
    public GameObject model;
    public bool isPolymorphed;

    // Start is called before the first frame update
    void Start()
    {
        isPolymorphed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            model.SetActive(!model.active);
            polymorphModel.SetActive(!polymorphModel.active);
        }
    }
}
