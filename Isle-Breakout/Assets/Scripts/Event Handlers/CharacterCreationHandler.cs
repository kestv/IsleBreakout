using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationHandler : MonoBehaviour
{
    public delegate void OnNameEntered(string name, int id);
    public OnNameEntered onNameEntered;
    public static CharacterCreationHandler Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
