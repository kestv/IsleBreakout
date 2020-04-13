using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsPanelController : MonoBehaviour
{
    public DependencyManager manager;
    public PlayerStatsController statsCtrl;

    public Transform strenghtSlot;
    public Transform speedSlot;
    public Transform wisdomSlot;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        statsCtrl = manager.getPlayer().GetComponent<PlayerStatsController>();

        strenghtSlot = transform.GetChild(0);
        speedSlot = transform.GetChild(1);
        wisdomSlot = transform.GetChild(2);

        UpdateStats();
    }

    public void UpdateStats()
    {
        UpdateStat(strenghtSlot, statsCtrl.strength.ToString());
        UpdateStat(speedSlot, statsCtrl.speed.ToString());
        UpdateStat(wisdomSlot, statsCtrl.wisdom.ToString());
    }

    public void UpdateStat(Transform stat, string value)
    {
        stat.GetChild(1).GetComponent<TextMeshProUGUI>().text = value;
    }
}
