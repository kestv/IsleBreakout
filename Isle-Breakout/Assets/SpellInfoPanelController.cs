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
        setDescriptionText(settings.getCooldown());
    }

    public void setItemName(string name)
    {
        itemName.GetComponent<TextMeshProUGUI>().text = name;
    }

    public void setSprite(Sprite sprite)
    {
        itemSprite.GetComponent<Image>().sprite = sprite;
    }

    public void setDescriptionText(float cooldown)
    {
        itemDescription.GetComponent<TextMeshProUGUI>().text = "Cooldown: " + cooldown.ToString();
    }
}
