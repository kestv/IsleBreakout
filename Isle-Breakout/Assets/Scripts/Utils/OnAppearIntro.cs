using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnAppearIntro : MonoBehaviour
{
    [SerializeField]GameObject camera1;
    [SerializeField]GameObject camera2;
    [SerializeField] GameObject skipButton;
    Animator animator;
    void Start()
    {
        camera2.SetActive(false);
        animator = GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
        StartCoroutine(IEIntro());
    }

    public void SkipIntro()
    {
        StartCoroutine(ChangeScenes());
        StopCoroutine(IEIntro());
    }

    IEnumerator ChangeScenes()
    {
        skipButton.SetActive(false);
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
        SceneManager.UnloadSceneAsync(1);
    }

    IEnumerator IEIntro()
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
        StartCoroutine(ChangeScenes());
    }
}
