using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellInfoPanelController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private Transform imagePanel;
    [SerializeField] private Transform textPanel;

    [Header("Objects")]
    [SerializeField] private Transform itemSprite;
    [SerializeField] private Transform itemName;
    [SerializeField] private Transform itemDescription;

    private SpellHolder spellHolder;

    public void InitPanel(SpellInfo settings)
    {
        setItemName(settings.getName());
        setSprite(settings.getSprite());
        setDescriptionText(settings);
    }

    public void setItemName(string name)
    {
        itemName.GetComponent<TextMeshProUGUI>().text = name;
    }

    public void setSprite(Sprite sprite)
    {
        itemSprite.GetComponent<Image>().sprite = sprite;
    }

    public void setDescriptionText(SpellInfo info)
    {
        float cooldown = info.getCooldown();
        float damage = info.getDamage();
        int type = info.getType();

        string text = "";
        if (type > 0)
        {
            switch (type)
            {
                case 0:
                    text += "Type: Offensive" + "\n";
                    break;
                default:
                    text += "Type: Neutral" + "\n";
                    break;
            }
        }

        if (cooldown > 0)
        {
            text += "Coooldown: " + cooldown.ToString() + "\n";
        }

        if (damage > 0)
        {
            text += "Damage: " + damage.ToString();
        }

        itemDescription.GetComponent<TextMeshProUGUI>().text = text;
    }
}
