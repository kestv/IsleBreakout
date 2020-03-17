using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour
{
    public GameObject spell;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSpell(GameObject spell, Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
