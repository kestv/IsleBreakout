using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellRewardController : MonoBehaviour
{
    public GameObject spellSprite;
    public GameObject acquiredInfo;
    public GameObject player;
    public GameObject confirmation;
    public GameObject rewardInfo;
    SpellController spellCtrl;
    GameObject spell;

    void Start()
    {
        player = GameObject.Find("PlayerInstance");
        gameObject.SetActive(false);
        spellCtrl = player.GetComponent<SpellController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (!spellCtrl.IsSlotTaken(1))
            {
                spellCtrl.SetSpell(spell, 1);
                Time.timeScale = 1f;
                gameObject.SetActive(false);
            }
            else
            {
                rewardInfo.SetActive(false);
                confirmation.SetActive(true);
                confirmation.GetComponent<RewardConfirmation>().SetSpell(this.spell, 1);
            }
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            if (!spellCtrl.IsSlotTaken(2))
            {
                spellCtrl.SetSpell(spell, 2);
                Time.timeScale = 1f;
                gameObject.SetActive(false);
            }
            else
            {
                rewardInfo.SetActive(false);
                confirmation.SetActive(true);
                confirmation.GetComponent<RewardConfirmation>().SetSpell(this.spell, 2);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }

    public void SetSpell(GameObject spell)
    {
        Time.timeScale = 0f;
        this.spell = spell;
        spellSprite.GetComponent<Image>().sprite = spell.GetComponent<SpellInfo>().image;
        acquiredInfo.GetComponent<Text>().text = spell.GetComponent<SpellInfo>().name + " acquired";
        rewardInfo.SetActive(true);
        rewardInfo.transform.GetChild(1).GetComponent<SpellHolder>().SetSpell(spell);

    }
}
