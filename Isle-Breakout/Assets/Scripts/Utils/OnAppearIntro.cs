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
    new AudioManager audio;
    void Start()
    {
        camera2.SetActive(false);
        animator = GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
        StartCoroutine(IEIntro());
        audio = GameObject.Find("Audio Source").GetComponent<AudioManager>();
        audio.Play("Waves");
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
        audio.Stop("Waves");
    }

    IEnumerator IEIntro()
    {
        yield return new WaitForSeconds(6f);
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
        audio.Play("Shake");
        camera2.GetComponent<StressReceiver>().InduceStress(1);

        StartCoroutine(ChangeScenes());
    }
}
