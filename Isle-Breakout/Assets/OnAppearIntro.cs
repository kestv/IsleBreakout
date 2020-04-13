using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnAppearIntro : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    void Start()
    {
        camera2.SetActive(false);
        GetComponent<Animator>().SetTrigger("FadeOut");
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        camera1.SetActive(false);
        camera2.SetActive(true);
        GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(10f);
        GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        SceneManager.UnloadScene(1);
        SceneManager.LoadScene(2);
    }
}
