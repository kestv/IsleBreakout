using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public int id;
    public Text name;
    public GameObject list;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetName()
    {
        if (name.text != "")
        {
            gameObject.SetActive(false);
            list.SetActive(true);
            CharacterCreationHandler.Instance.onNameEntered(name.text, id);
        }
    }
}
