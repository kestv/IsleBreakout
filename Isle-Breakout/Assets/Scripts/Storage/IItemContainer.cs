using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemContainer
{
    bool ContainsItem(string name);
    bool RemoveItem(string name);
    bool AddItem(GameObject go);
    bool isFull();
    int ItemCount(string name);
}
