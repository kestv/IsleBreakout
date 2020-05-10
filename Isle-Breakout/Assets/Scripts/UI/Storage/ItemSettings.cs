using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ItemSettings : MonoBehaviour
{
    [Header("Item settings")]
    [SerializeField] private int itemID;            //Item ID used for indentification
    [SerializeField] private string itemName;       //Item name used in scripts
    [SerializeField] private string description;    //Item description
    [SerializeField] private Sprite sprite;         //Item sprite (image) used for item containers such as inventory or chests  
    [SerializeField] private GameObject model;      //Item model used in UI (different scale, position, pivot, etc.)
    [SerializeField] ScriptableObject equip;        //Item equipment component
    [SerializeField] private float dropChance;      //Item drop chance

    public int getItemID()
    { return itemID; }

    public void setItemID(int itemID)
    { this.itemID = itemID; }

    public string getName()
    { return itemName; }

    public void setName(string name)
    { this.itemName = name; }

    public string getDescription()
    { return description; }

    public void setDescription(string description)
    { this.description = description; }

    public Sprite getSprite()
    { return sprite; }

    public void setSprite(Sprite sprite)
    { this.sprite = sprite; }

    public GameObject getModel()
    { return model; }

    public void setModel(GameObject model)
    { this.model = model; }

    public ScriptableObject getEquip()
    { return equip; }

    public void setEquip(ScriptableObject equip)
    { this.equip = equip; }

    public float getDropChance()
    { return dropChance; }

    public void setDropChance(float dropChance)
    { this.dropChance = dropChance; }
}
