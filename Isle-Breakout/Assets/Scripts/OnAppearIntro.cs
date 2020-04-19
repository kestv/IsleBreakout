using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnAppearIntro : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    Animator animator;
    void Start()
    {
        camera2.SetActive(false);
        animator = GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(10f);
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        camera1.SetActive(false);
        camera2.SetActive(true);
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(4f);
        animator.SetTrigger("Blink");
        yield return new WaitForSeconds(4f);
        animator.SetTrigger("Blink");
        yield return new WaitForSeconds(6f);
        camera2.GetComponent<StressReceiver>().InduceStress(1);
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
        SceneManager.UnloadSceneAsync(1);
    }
}
