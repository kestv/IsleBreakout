using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public PlayerStatsController statsCtrl;
    public PlayerLevelController levelCtrl;
    public PlayerCombatController combatCtrl;
    public PlayerMovementController movementCtrl;

    public GameObject menu;
    bool menuOpen;
    void Start()
    {
        menu = GameObject.Find("Menu");
        menu.SetActive(false);
        menuOpen = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            statsCtrl.Open();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(combatCtrl.target == null)
            {
                if (menuOpen)
                {
                    menu.SetActive(false);
                    Time.timeScale = 1f;
                    menuOpen = false;
                    movementCtrl.enabled = true;
                }
                else
                {
                    menu.SetActive(true);
                    Time.timeScale = 0f;
                    menuOpen = true;
                    movementCtrl.enabled = false;
                }
            }
        }
    }

    public void SaveGame()
    {
        PlayerData player = new PlayerData(Player.name, levelCtrl.level, levelCtrl.totalXp, Player.id);

        DataSystem.Save(player);
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        menuOpen = false;
        movementCtrl.enabled = true;
    }

    public void MainMenu()
    {
        SaveGame();
        SceneManager.LoadScene(0);
        SceneManager.UnloadScene(1);
    }
}
