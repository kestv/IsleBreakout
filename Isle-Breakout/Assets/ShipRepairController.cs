using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRepairController : MonoBehaviour
{
    public GameObject objectPanel;
    public GameObject recipeListPanel;
    public GameObject recipePartPanel;

    private void Start()
    {
        objectPanel = transform.GetChild(0).gameObject;
        recipeListPanel = transform.GetChild(1).GetChild(0).gameObject;
        recipePartPanel = transform.GetChild(2).gameObject;
    }
}
