using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestScript : MonoBehaviour
{
    public GameObject item;
    public GameObject item2;
    public GameObject item3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            item = PhotonNetwork.Instantiate(item.name, item.transform.position, Quaternion.identity, 0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            item2 = PhotonNetwork.Instantiate(item2.name, item2.transform.position, Quaternion.identity, 0);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            item3 = PhotonNetwork.Instantiate(item3.name, item3.transform.position, Quaternion.identity, 0);
        }
    }
}
