using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObjectController : MonoBehaviour
{
    //-----------------------VARIABLES-----------------------
    private DependencyManager manager;
    private GameObject shipRenderer;
    private PanelObjectRotator rotator;

    //---------------------UNITY METHODS---------------------
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        shipRenderer = manager.getCanvasShipRenderer();
        rotator = GetComponent<PanelObjectRotator>();
    }

    //------------------------METHODS------------------------
    public void InitObject(GameObject go)
    {
        removeChild();
        setChild(go);
    }

    public void setChild(GameObject go)
    {
        GameObject child = Instantiate(go.GetComponent<ItemSettings>().getModel());
        child.transform.SetParent(shipRenderer.transform.GetChild(0).GetChild(0), false);
        rotator.setObject(child);
    }

    public void removeChild()
    {
        Transform modelPanel = shipRenderer.transform.GetChild(0).GetChild(0);

        if (modelPanel.childCount > 0)
        {
            foreach(Transform child in modelPanel)
            {
                Destroy(child.gameObject);
                rotator.setObject(null);
            }
        }
    }
}
