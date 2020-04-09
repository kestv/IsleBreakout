using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public GameObject prefab;
    public EquipFinder eqpFinder;
    public List<int> xpath;

    public GameObject item;

    private void Start()
    {
        eqpFinder = prefab.GetComponent<EquipFinder>();
        eqpFinder.FindItemPath(item);
        xpath = eqpFinder.getPathIndexes();
        ChangeItemState(true);
    }

    public void ChangeItemState(bool state)
    {
        Transform t = transform;
        foreach (int i in xpath)
        {
            t = t.GetChild(i);
        }
        t.gameObject.SetActive(state);
    }
}
