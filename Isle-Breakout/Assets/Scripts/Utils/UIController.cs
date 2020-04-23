using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerStatsController statsCtrl;
    public PlayerLevelController levelCtrl;
    public PlayerCombatController combatCtrl;
    public PlayerMovementController movementCtrl;

    public GameObject menu;
    bool menuOpen;

    public GameObject panel;
    public GameObject endGamePanel;
    float timer;


    public void EndGame()
    {
        StartCoroutine(InitEndGame());
    }

    IEnumerator InitEndGame()
    {
        panel.SetActive(true);
        SaveGame(true);
        yield return new WaitForSeconds(2f);
        panel.GetComponent<OnSpawn>().FadeIn();
        yield return new WaitForSeconds(4f);
        endGamePanel.SetActive(true);
        var text = endGamePanel.transform.GetChild(0).GetComponent<Text>();
        var xp = GetComponent<PlayerLevelController>().currentGameXp;
        var time = GetComponent<ProgressTracker>().GetTime();
        TimeSpan timeSpan = time < 60 ? TimeSpan.FromSeconds(Convert.ToDouble(time + 60)) : TimeSpan.FromSeconds(Convert.ToDouble(time));
        text.text = string.Format("Time spent: {0} h {1} min\nXp gained: {2}", timeSpan.Hours, timeSpan.Minutes - timeSpan.Hours * 60, xp);
    }

    void Start()
    {
        endGamePanel.SetActive(false);
        Debug.Log(gameObject.name);
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

    public void SaveGame(bool ended)
    {
        var n = ended ? 1 : 0;
        var time = GetComponent<ProgressTracker>().GetTime();
        PlayerData player = new PlayerData(Player.name, levelCtrl.level, levelCtrl.totalXp, Player.id, Player.totalGamesPlayed + n, Player.totalTimeSpent + time);

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
        SaveGame(false);
        SceneManager.LoadScene(0);
    }
}
