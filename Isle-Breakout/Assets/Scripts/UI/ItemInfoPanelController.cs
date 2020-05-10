using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfoPanelController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private Transform descriptionPanel;
    [SerializeField] private Transform statsPanel;

    [Header("Objects")]
    [SerializeField] private Transform itemName;
    [SerializeField] private Transform itemSprite;
    [SerializeField] private Transform itemDescription;
    [SerializeField] private Transform itemStats;

    public void InitPanel(ItemSettings settings)
    {
        setItemName(settings.getName());
        setSprite(settings.getSprite());
        setDescriptionText(settings.getDescription());
        setItemStatsText(settings.getEquip());
    }

    public void InitPanel(ItemSettings settings, ItemConsumable consumable)
    {
        setItemName(settings.getName());
        setSprite(settings.getSprite());
        setDescriptionText(consumable.getConsumableStats());
        setItemStatsText(settings.getEquip());
    }

    public void setItemName(string name)
    {
        itemName.GetComponent<TextMeshProUGUI>().text = name;
    }

    public void setSprite(Sprite sprite)
    {
        itemSprite.GetComponent<Image>().sprite = sprite;
    }

    public void setDescriptionText(string text)
    {
        if (text == null || text == "")
        {
            descriptionPanel.gameObject.SetActive(false);
        }
        else
        {
            itemDescription.GetComponent<TextMeshProUGUI>().text = text;
        }        
    }

    public void setItemStatsText(ScriptableObject scriptableObject)
    {
        IArmor equip = (IArmor)scriptableObject;

        if(equip == null)
        {
            statsPanel.gameObject.SetActive(false);
        }
        else
        {
            string stats = "";
            float healthpoints = equip.getHP();
            float strength = equip.getStrength();
            float speed = equip.getSpeed();
            float wisdom = equip.getWisdom();

            //Healthpoints
            if(healthpoints != 0)
            {
                if(healthpoints > 0)
                {
                    stats += "HP +" + healthpoints + "\n";
                }
                else
                {
                    stats += "HP " + healthpoints + "\n";
                }
            }
            //Strength
            if (strength != 0)
            {
                if (strength > 0)
                {
                    stats += "Strength +" + strength + "\n";
                }
                else
                {
                    stats += "Strength " + strength + "\n";
                }
            }
            //Speed
            if (speed != 0)
            {
                if (speed > 0)
                {
                    stats += "Speed +" + strength + "\n";
                }
                else
                {
                    stats += "Speed " + strength + "\n";
                }
            }
            //Wisdom
            if (wisdom != 0)
            {
                if (speed > 0)
                {
                    stats += "Wisdom +" + wisdom;
                }
                else
                {
                    stats += "Wisdom " + wisdom;
                }
            }

            if(stats != null || stats != "")
            {
                itemStats.GetComponent<TextMeshProUGUI>().text = stats;
            }
            else
            {
                statsPanel.gameObject.SetActive(false);
            }            
        }
    }
}
