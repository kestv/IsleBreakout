using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ItemSettings : MonoBehaviour
{
    public int itemID;      //Item display name
    public string name;     //Item name used in scripts
    public Sprite sprite;   //Item sprite (image) used for item containers such as inventory or chests

    //--------------------------------------
    public int getItemID()
    { return itemID; }

    public void setItemID(int itemID)
    { this.itemID = itemID; }

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
