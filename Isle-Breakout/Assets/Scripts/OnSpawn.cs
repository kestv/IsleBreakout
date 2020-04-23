using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSpawn : MonoBehaviour
{
    void Start()
    {
        FadeOut();
    }

    public void FadeOut()
    {
        StartCoroutine(Fade());
    }

    public void FadeIn()
    {
        GetComponent<Animator>().SetTrigger("FadeIn");
    }

    IEnumerator Fade()
    {
        GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}
