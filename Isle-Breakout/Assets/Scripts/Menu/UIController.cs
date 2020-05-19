using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    PlayerStatsController statsCtrl;
    PlayerLevelController levelCtrl;
    PlayerCombatController combatCtrl;
    PlayerMovementController movementCtrl;

    GameObject menu;
    bool menuOpen;

    [SerializeField]
    GameObject panel;
    [SerializeField]
    GameObject endGamePanel;
    float timer;

    void Start()
    {
        statsCtrl = GetComponent<PlayerStatsController>();
        levelCtrl = GetComponent<PlayerLevelController>();
        combatCtrl = GetComponent<PlayerCombatController>();
        movementCtrl = GetComponent<PlayerMovementController>();
        endGamePanel.SetActive(false);
        Debug.Log(gameObject.name);
        menu = GameObject.Find("Menu");
        menu.SetActive(false);
        menuOpen = false;
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        StartCoroutine(IEInitEndGame());
    }

    IEnumerator IEInitEndGame()
    {
        GameObject.Find("Manager").GetComponent<DependencyManager>().getCanvas().gameObject.SetActive(false);
        panel.SetActive(true);
        SaveGame(true);
        yield return new WaitForSeconds(2f);
        panel.GetComponent<OnSpawn>().FadeIn();
        yield return new WaitForSeconds(4f);
        endGamePanel.SetActive(true);
        var text = endGamePanel.transform.GetChild(0).GetComponent<Text>();
        var xp = levelCtrl.GetCurrentGameXp();
        var time = GetComponent<ProgressTracker>().GetTime();
        TimeSpan timeSpan = time < 60 ? TimeSpan.FromSeconds(Convert.ToDouble(time + 60)) : TimeSpan.FromSeconds(Convert.ToDouble(time));
        text.text = string.Format("Time spent: {0} h {1} min\nXp gained: {2}", timeSpan.Hours, timeSpan.Minutes - timeSpan.Hours * 60, xp);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(combatCtrl.GetTarget() == null)
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
            else
            {
                combatCtrl.CancelTarget();
            }
        }
    }

    public void SaveGame(bool ended)
    {
        var n = ended ? 1 : 0;
        var time = GetComponent<ProgressTracker>().GetTime();
        PlayerData player = new PlayerData(Player.name, levelCtrl.GetLevel(), levelCtrl.GetTotalXp(), Player.id, Player.totalGamesPlayed + n, Player.totalTimeSpent + time);

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
        SaveGame(true);
        SceneManager.LoadScene(0);
        SceneManager.UnloadSceneAsync(2);
    }
}
