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
        StartCoroutine(IEFade());
    }

    public void FadeIn()
    {
        GetComponent<Animator>().SetTrigger("FadeIn");
    }

    IEnumerator IEFade()
    {
        GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}
