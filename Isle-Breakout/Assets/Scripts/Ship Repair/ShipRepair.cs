using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRepair : MonoBehaviour
{
    [Header("Children transforms")]
    [SerializeField] private Transform partRenderer;
    [SerializeField] private Transform recipeScrollPanel;
    [SerializeField] private Transform recipePartPanel;
    [SerializeField] private Transform scrollbar;

    public Transform getPartRenderer()
    { return partRenderer; }

    public Transform getRecipeScrollPanel()
    { return recipeScrollPanel; }

    public Transform getRecipePartPanel()
    { return recipePartPanel; }

    public Transform getScrollbar()
    { return scrollbar; }

}
