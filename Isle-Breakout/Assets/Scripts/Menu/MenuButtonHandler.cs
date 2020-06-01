using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour
{
    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject charSelect;
    [SerializeField]
    GameObject options;
    [SerializeField]
    GameObject panel;

    Coroutine co;
    void Start()
    {
        panel.SetActive(false);
        charSelect.SetActive(false);
        options.SetActive(false);
        Time.timeScale = 1;
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
        print("enter world");
        panel.SetActive(true);
        GetComponent<Animator>().SetTrigger("FadeIn");

        co = StartCoroutine(IESwitchScenes());
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

    IEnumerator IESwitchScenes()
    {
        print("loading scene");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Intro");
        SceneManager.UnloadSceneAsync("Menu");
        StopCoroutine();
    }

    void StopCoroutine()
    {
        StopCoroutine(co);
    }
}
