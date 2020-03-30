using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellHolder : MonoBehaviour
{
    public GameObject spell;
    public Image image;
    public bool cooldown;
    void Start()
    {
        cooldown = false;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown && image != null)
        {
            image.fillAmount += 1f/spell.GetComponent<ProjectileMoveScript>().cooldown * Time.deltaTime;
            if (image.fillAmount >= 1) cooldown = false;
        }
    }

    public void setSpell(GameObject spell, Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
