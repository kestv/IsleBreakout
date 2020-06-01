using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFiller : MonoBehaviour
{
    [Header("Panel offset")]
    [SerializeField] private float ofssetX;
    [SerializeField] private float offsetY;

    private DependencyManager manager;
    private PlayerInventory inventory;
    private Transform player;

    private GameObject caller;
    private GameObject item;
    private float gatherTime;
    private int mineCount;

    private Image progressImage;    //Image used to display mining progress
    private float modifier;         //Used for image.fillAmount visualization; Low value = image edited in big chunks and vice versa;
    private float elapsedTime;      //Time elapsed while mining the resource
    private float stepSize;         //Size of the step used for editing the fillAmount parameter of image
    private bool mining;            //Boolean showing whether the resource is being mined or not

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        inventory = manager.getPlayer().GetComponent<PlayerInventory>();
        player = manager.getPlayer().transform;
        progressImage = GetComponent<Image>();
    }

    void Update()
    {
        if (mining)
        {
            StartMining();
        }
    }

    public void StartMining()
    {
        player.GetComponent<Animator>().SetBool("isMining", true);
        player.GetComponent<PlayerMovementController>().enabled = false;

        elapsedTime += Time.deltaTime;

        if (gatherTime >= elapsedTime)
        {
            //image.fillAmount += 1f / spell.GetComponent<SpellInfo>().cooldown * Time.deltaTime;
            progressImage.fillAmount += 1f / gatherTime * Time.deltaTime;
            //if (elapsedTime >= 1f / modifier)
            //{
            //    elapsedTime = elapsedTime % (1f / modifier);
            //    progressImage.fillAmount += stepSize;
            //    gatherTime -= 1 / modifier;
            //}
        }
        else
        {
            mining = false;

            player.GetComponent<Animator>().SetBool("isMining", false);
            player.GetComponent<PlayerMovementController>().enabled = true;
            inventory.AddItem(Instantiate(item));

            gameObject.SetActive(false);
            caller.GetComponent<ResourceGatherer>().OnMineSuccess();

            if (caller.GetComponent<ResourceGatherer>().getGatherCount() <= 0)
            {
                player.GetComponent<PlayerTriggerHandler>().RemoveTrigger(caller);
                Destroy(caller);
            }
            else
            {
                player.GetComponent<PlayerTriggerHandler>().UpdateTriggerMessage();
            }
        }
    }

    public void InitVariables(GameObject caller, float gatherTime, GameObject item, int mineCount)
    {
        //Camera
        Vector3 posOffset = new Vector3(ofssetX, offsetY, 0);
        transform.position = Camera.main.WorldToScreenPoint(player.position + posOffset);

        //Self-related variables
        modifier = 100;
        stepSize = 1 / gatherTime / modifier;
        progressImage.fillAmount = 0;
        elapsedTime = 0;
        mining = true;

        //ResourceGatherer variables
        this.caller = caller;
        this.gatherTime = gatherTime;
        this.item = item;
        this.mineCount = mineCount;
    }
}
