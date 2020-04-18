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
        image.sprite = spell.GetComponent<SpellInfo>().image;
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown && image != null)
        {
            image.fillAmount += 1f/spell.GetComponent<SpellInfo>().cooldown * Time.deltaTime;
            if (image.fillAmount >= 1) cooldown = false;
        }
    }

    public void SetSpell(GameObject spell)
    {
        image.sprite = spell.GetComponent<SpellInfo>().image;
        this.spell = spell;
    }

    public GameObject getSpell()
    { return spell; }
}
