using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    public GameObject menu;
    public GameObject charSelect;
    public GameObject options;
    public GameObject panel;
    void Start()
    {
        panel.SetActive(false);
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
        panel.SetActive(true);
        GetComponent<Animator>().SetTrigger("FadeIn");
        StartCoroutine(SwitchScenes());
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

    IEnumerator SwitchScenes()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
        SceneManager.UnloadSceneAsync(0);
    }
}
