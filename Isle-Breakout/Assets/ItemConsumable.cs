using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConsumable : MonoBehaviour
{
    [Header("Consumable parameters")]
    [SerializeField] private float restoreHP;
    [SerializeField] private float restoreHunger;

    private PlayerHealthController playerHealthCtrl;

    private void Start()
    {
        DependencyManager manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        playerHealthCtrl = manager.getPlayer().GetComponent<PlayerHealthController>();
    }

    public void Consume()
    {
        if(restoreHP > 0)
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
        text += "HP: +" + restoreHP.ToString() + "\n";
        text += "Hunger: +" + restoreHunger.ToString();
        return text;
    }
}
