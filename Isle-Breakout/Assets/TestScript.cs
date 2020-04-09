using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform itemPath;
    public GameObject item;

    public string TransformRoot;

    public List<string> transformPath;
    public List<int> indexes;

    void Start()
    {
        TransformRoot = "SAMPLE_MODEL";
        transformPath = new List<string>();
        indexes = new List<int>();
        
        //findParent(item.transform);
        //DeactivateItem();
    }

    public bool findParent(GameObject go)
    {
        indexes.Insert(0, go.transform.GetSiblingIndex());

        if (go.transform.parent.name == transform.name)
        {
            return true;
        }
        else
        {
            findParent(go.transform.parent.gameObject);
        }
        return false;
    }

    public List<int> getPath()
    { return indexes; }

    public void DeactivateItem()
    {
        Transform t = transform;
        foreach(int i in indexes)
        {
            t = t.GetChild(i);
        }
        t.gameObject.SetActive(false);
    }

    public void Print()
    {
        Debug.Log(transformPath);
    }
}
