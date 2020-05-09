using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameEnterController : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    Text name;
    [SerializeField]
    GameObject list;

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

    public void SetId(int id)
    {
        this.id = id;
    }
}
