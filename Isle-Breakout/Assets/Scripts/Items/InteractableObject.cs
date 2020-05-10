using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject newObjectPrefab;
    [SerializeField] private string displayMessage;
    [SerializeField] private float xPosition;
    [SerializeField] private float yPosition;
    [SerializeField] private float zPosition;

    private Vector3 newObjectPosition;

    private void Start()
    {
        newObjectPosition = new Vector3(xPosition, yPosition, zPosition);
    }

    public void TransformToNewObject()
    {
        Quaternion quaternion = new Quaternion(newObjectPrefab.transform.rotation.x, newObjectPrefab.transform.rotation.y, newObjectPrefab.transform.rotation.z, 0);
        GameObject newObject = Instantiate(newObjectPrefab, GameObject.Find("Map").transform, false);
        newObject.transform.position = newObjectPosition;
        gameObject.SetActive(false);
    }

    public string getDisplaymessage()
    { return displayMessage; }
}
