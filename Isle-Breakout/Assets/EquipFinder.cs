using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFinder : MonoBehaviour
{
    public List<int> pathIndexes;
    public GameObject item;

    void Start()
    {
        pathIndexes = new List<int>();
    }

    public bool FindItemPath(GameObject go)
    {
        pathIndexes.Insert(0, go.transform.GetSiblingIndex());

        if (go.transform.parent.name == transform.name)
        {
            return true;
        }
        else
        {
            FindItemPath(go.transform.parent.gameObject);
        }
        return false;
    }

    public void ClearIndexes()
    {
        pathIndexes = new List<int>();
    }

    public List<int> getPathIndexes()
    { return pathIndexes; }

    public void setPathIndexes(List<int> pathIndexes)
    { this.pathIndexes = pathIndexes; }

    public void setItem(GameObject go)
    { this.item = go; }

}
