using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    AudioManager audio;
    bool running;
    Animator anim;
    private void Start()
    {
        audio = GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
    }
    public void IsRunning(bool isRunning)
    {
        var anim = transform.GetComponent<Animator>();
        if (transform.GetComponent<Animator>().GetBool("isRunning") != true)
        {
            if (audio != null && !audio.IsPlaying("Running"))
            {
                audio.Play("Running");
            }
            anim.SetBool("isRunning", isRunning);
            anim.SetBool("isIdling", !isRunning);
            anim.SetBool("isAttacking", !isRunning);
            anim.SetBool("isWalking", !isRunning);
        }
    }

    public void IsIdling(bool isIdling)
    {
        var anim = transform.GetComponent<Animator>();
        if (anim.GetBool("isIdling") != true)
        {
            anim.SetBool("isIdling", isIdling);
            anim.SetBool("isRunning", !isIdling);
            anim.SetBool("isAttacking", !isIdling);
            anim.SetBool("isWalking", !isIdling);
        }
    }

    public void IsAttacking(bool isAttacking)
    {
        var anim = transform.GetComponent<Animator>();
        if (transform.GetComponent<Animator>().GetBool("isAttacking") != true)
        {
            anim.SetBool("isAttacking", isAttacking);
            anim.SetBool("isRunning", !isAttacking);
            anim.SetBool("isIdling", !isAttacking);
            anim.SetBool("isWalking", !isAttacking);
        }
    }

    public void IsWalking(bool isWalking)
    {
        if (audio != null && audio.IsPlaying("Running"))
        {
            audio.Stop("Running");
        }
        var anim = transform.GetComponent<Animator>();
        if (transform.GetComponent<Animator>().GetBool("isWalking") != true)
        {
            anim.SetBool("isAttacking", !isWalking);
            anim.SetBool("isRunning", !isWalking);
            anim.SetBool("isIdling", !isWalking);
            anim.SetBool("isWalking", isWalking);
        }
    }
}
