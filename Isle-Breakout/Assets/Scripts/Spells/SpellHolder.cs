using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellHolder : MonoBehaviour
{
    [SerializeField]GameObject spell;
    [SerializeField]Image image;
    bool cooldown;

    void Start()
    {
        cooldown = false;
        if(spell != null)
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
        image.color = new Color(255, 255, 255, 255);
        this.spell = spell;
    }

    public GameObject GetSpell()
    { 
        return spell; 
    }

    public Image GetImage()
    {
        return this.image;
    }

    public bool IsOnCooldown()
    {
        return this.cooldown;
    }

    public void SetOnCooldown(bool cd)
    {
        this.cooldown = cd;
    }
}
