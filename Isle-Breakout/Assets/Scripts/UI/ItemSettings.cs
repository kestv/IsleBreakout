using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSettings : MonoBehaviour
{
    public string label;    //Item display name
    public string name;     //Item name used in scripts
    public Sprite sprite;

    //--------------------------------------
    public string getLabel()
    { return label; }

    public void setLabel(string label)
    { this.label = label; }

    public string getName()
    { return name; }

    public void setName(string name)
    { this.name = name; }

    public Sprite getSprite()
    { return sprite; }

    public void setSprite(Sprite sprite)
    { this.sprite = sprite; }
    //--------------------------------------
}
