using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public DependencyManager manager;
    public ArmorEquipper eqpr;
    

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        eqpr = manager.getPlayer().GetComponent<ArmorEquipper>();
    }

    public void EQP()
    {
        eqpr.EQP();
    }

    public void UNEQP()
    {
        eqpr.UNEQP();
    }
}
