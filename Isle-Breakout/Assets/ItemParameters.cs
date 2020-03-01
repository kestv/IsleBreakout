using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ItemParameters : MonoBehaviourPun
{
    public string itemName;
    public Image itemImage;

    public string getName()
    { return itemName; }

    public void setName(string itemName)
    { this.itemName = itemName; }

    public void ChangeParent(Transform parent)
    {
        this.gameObject.transform.parent = parent;
        //PhotonNetwork.Destroy(this.gameObject);
    }

    public void Disable()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1000, this.gameObject.transform.position.z);
        //TODO - Disable render and box collider instead of changing Y coordinate

        //this.gameObject.GetComponent<Renderer>().enabled = false;   
        //this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void Enable(Vector3 playerPosition)
    {
        this.gameObject.transform.position = playerPosition;
    }
}
