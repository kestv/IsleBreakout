using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftItemSlotController : MonoBehaviour
{
    public GameObject slotSprite;
    public GameObject slotText;
    public GameObject slotCountText;

    public Sprite sprite;
    public string text;
    public string countText;

    private void Start()
    {        
        slotSprite = transform.GetChild(0).GetChild(0).gameObject;
        slotText = transform.GetChild(1).GetChild(0).gameObject;
        slotCountText = transform.GetChild(2).GetChild(0).gameObject;

        setSlotSprite(sprite);
        setSlotText(text);
        setSlotCountText(countText);
    }

    public Sprite getSlotSprite()
    { return slotSprite.GetComponent<Image>().sprite; }

    public void setSlotSprite(Sprite sprite)
    { this.slotSprite.GetComponent<Image>().sprite = sprite; }

    public string getSlotText()
    { return slotText.GetComponent<TextMeshProUGUI>().text; }

    public void setSlotText(string text)
    { this.slotText.GetComponent<TextMeshProUGUI>().text = text; }

    public string getSlotCountText()
    { return slotCountText.GetComponent<TextMeshProUGUI>().text; }

    public void setSlotCountText(string count)
    { this.slotCountText.GetComponent<TextMeshProUGUI>().text = count; }

    public Sprite getSprite()
    { return sprite; }

    public void setSprite(Sprite sprite)
    { this.sprite = sprite; }

    public string getText()
    { return text; }

    public void setText(string text)
    { this.text = text; }

    public string getCountText()
    { return countText; }

    public void setCountText(string countText)
    { this.countText = countText; }
}
