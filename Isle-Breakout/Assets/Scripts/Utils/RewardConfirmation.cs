using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardConfirmation : MonoBehaviour
{
    SpellController spellCtrl;
    GameObject spell;
    public GameObject player;
    public GameObject reward;
    public GameObject lastSpell;
    public GameObject newSpell;
    Text text;
    int slot;

    private void Start()
    {
        player = GameObject.Find("PlayerInstance");
        gameObject.SetActive(false);
        spellCtrl = player.GetComponent<SpellController>();
        text = GetComponent<Text>();
    }
    public void Confirm()
    {
        spellCtrl.SetSpell(spell, slot);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        reward.SetActive(false);
    }

    public void SetSpell(GameObject spell, int slot)
    {
        this.spell = spell;
        this.slot = slot;
        newSpell.GetComponent<Text>().text = spell.GetComponent<SpellInfo>().name;
        lastSpell.GetComponent<Text>().text = spellCtrl.GetSpellName(slot);
    }

    public void Cancel()
    {
        Time.timeScale = 1f;
        reward.SetActive(false);
        gameObject.SetActive(false);
    }
}
