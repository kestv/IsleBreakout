using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsPanelController : MonoBehaviour
{
    private DependencyManager manager;
    private PlayerStatsController statsCtrl;

    private Transform strenghtSlot;
    private Transform speedSlot;
    private Transform wisdomSlot;

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
        UpdateStat(strenghtSlot, statsCtrl.GetStrength().ToString());
        UpdateStat(speedSlot, statsCtrl.GetSpeed().ToString());
        UpdateStat(wisdomSlot, statsCtrl.GetWisdom().ToString());
    }

    public void UpdateStat(Transform stat, string value)
    {
        stat.GetChild(1).GetComponent<TextMeshProUGUI>().text = value;
    }
}
