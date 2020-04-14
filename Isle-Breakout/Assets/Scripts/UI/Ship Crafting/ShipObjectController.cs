using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObjectController : MonoBehaviour
{
    //-----------------------VARIABLES-----------------------
    public PanelObjectRotator rotator;

    //---------------------UNITY METHODS---------------------
    private void Start()
    {
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
        child.transform.SetParent(transform, false);
        rotator.setObject(child);
    }

    public void removeChild()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            rotator.setObject(null);
        }
    }
}
