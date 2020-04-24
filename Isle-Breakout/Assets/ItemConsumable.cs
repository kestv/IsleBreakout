using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConsumable : MonoBehaviour
{
    [Header("Consumable parameters")]
    [SerializeField] private float restoreHP;
    [SerializeField] private float restoreHunger;

    public void Consume()
    {
        PlayerHealthController playerHealthCtrl = GameObject.Find("Manager").GetComponent<DependencyManager>().getPlayer().GetComponent<PlayerHealthController>();

        if (restoreHP > 0)
        {
            playerHealthCtrl.Heal(restoreHP);
        }
        if(restoreHunger > 0)
        {
            playerHealthCtrl.Eat(restoreHunger);
        }
    }

    public string getConsumableStats()
    {
        string text = "";

        if(restoreHP > 0)
        {
            text += "HP: +" + restoreHP.ToString() + "\n";
        }
        if(restoreHunger > 0)
        {
            text += "Hunger: +" + restoreHunger.ToString();
        }
        
        return text;
    }
}
