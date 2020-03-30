using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyManager : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    public int inventorySize;

    private void Start()
    {
        //player = GameObject.Find("PlayerInstance");
        player = Instantiate(player, new Vector3(394, 9, -408), player.transform.rotation);
        GameObject.Find("Main Camera").GetComponent<CameraMovement>().lookAt = player.transform;
        player.GetComponent<PlayerMovementController>().cam = GameObject.Find("Main Camera");
        player.GetComponent<PlayerHealthController>().enabled = false;
        player.GetComponent<PlayerCombatController>().enabled = false;
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<PlayerSkillController>().enabled = false;
        player.GetComponent<PlayerLevelController>().enabled = false;
        player.GetComponent<PlayerSkillController>().enabled = false;
        player.GetComponent<TriggerController>().enabled = false;
        player.GetComponent<PlayerStatsController>().enabled = false;
        player.GetComponent<SpellController>().enabled = false;
        canvas = Instantiate(canvas);
        inventorySize = 6;
    }

    public GameObject getPlayer()
    { return player; }

    public void setPlayer(GameObject player)
    { this.player = player; }

    public GameObject getCanvas()
    { return canvas; }

    public void setCanvas(GameObject canvas)
    { this.canvas = canvas; }

    public int getInventorySize()
    { return inventorySize; }

    public void setInventorySize(int inventorySize)
    { this.inventorySize = inventorySize; }
}
