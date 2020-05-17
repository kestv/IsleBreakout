using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelObjectRotator : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [Header("Object that will be rotated")]
    [SerializeField] private GameObject go;

    private float lastFramePosition;
    private float sensitivity;

    private void Start()
    {
        sensitivity = 0.25f;       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastFramePosition = Input.mousePosition.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = Input.mousePosition.x - lastFramePosition;
        lastFramePosition = Input.mousePosition.x;

        go.transform.RotateAround(go.transform.position, go.transform.up, delta * sensitivity);
    }

    public GameObject getObject()
    { return go; }

    public void setObject(GameObject go)
    { this.go = go; }

}
