using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    public GameObject menu;
    public GameObject charSelect;
    public GameObject options;
    void Start()
    {
        charSelect.SetActive(false);
        options.SetActive(false);
    }

    public void NewGame()
    {
        menu.SetActive(false);
        charSelect.SetActive(true);
    }

    public void BackToMenu()
    {
        menu.SetActive(true);
        charSelect.SetActive(false);
    }

    public void EnterWorld()
    {
        SceneManager.LoadScene(1);
        SceneManager.UnloadScene(0);
    }

    public void Options()
    {
        menu.SetActive(false);
        options.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
