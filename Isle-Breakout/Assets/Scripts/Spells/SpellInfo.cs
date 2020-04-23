using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInfo : MonoBehaviour
{
    public Sprite image;
    public string name;
    public float cooldown;
    public int type;

    public float damage;
    
    public Sprite getSprite()
    { return image; }

    public string getName()
    { return name; }

    public float getCooldown()
    { return cooldown; }

    public int getType()
    { return type; }

    public float getDamage()
    { return damage; }
}
