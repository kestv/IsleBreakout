using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGatherer : MonoBehaviour
{
    [Header("Resource parameters")]
    [SerializeField] private GameObject gatherItem;     //The item you get when you gather the resource
    [SerializeField] private float gatherTime;          //Timer required to gather resource
    [SerializeField] private int gatherCount;           //Count of times you can gather the resource before it disappears

    private DependencyManager manager;
    private GameObject progressPanel;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        progressPanel = manager.getResourceGatherImage();

        if (gatherCount <= 0)
        {
            gatherCount = 1;
        }
    }

    public void Gather()
    {
        ImageFiller script = progressPanel.GetComponent<ImageFiller>();
        script.InitVariables(gameObject, gatherTime, gatherItem, gatherCount);
        progressPanel.transform.parent.gameObject.SetActive(true);
    }

    public void InitCraftingBench()
    {

    }

    public void OnMineSuccess()
    {
        gatherCount--;
    }

    public GameObject getGatherItem()
    { return gatherItem; }

    public void setGatherItem(GameObject gatherItem)
    { this.gatherItem = gatherItem; }

    public float getGatherTime()
    { return gatherTime; }

    public void setGatherTime(float gatherTime)
    { this.gatherTime = gatherTime; }

    public int getGatherCount()
    { return gatherCount; }

    public void setGatherCount(int gatherCount)
    { this.gatherCount = gatherCount; }
}
