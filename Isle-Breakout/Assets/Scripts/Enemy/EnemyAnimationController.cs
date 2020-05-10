using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public void IsRunning(bool isRunning)
    {
        if (transform.GetComponent<Animator>().GetBool("isRunning") != true)
        {
            transform.GetComponent<Animator>().SetBool("isRunning", isRunning);
            transform.GetComponent<Animator>().SetBool("isIdling", !isRunning);
            transform.GetComponent<Animator>().SetBool("isAttacking", !isRunning);
            transform.GetComponent<Animator>().SetBool("isWalking", !isRunning);
        }
    }

    public void IsIdling(bool isIdling)
    {
        if (transform.GetComponent<Animator>().GetBool("isIdling") != true)
        {
            transform.GetComponent<Animator>().SetBool("isIdling", isIdling);
            transform.GetComponent<Animator>().SetBool("isRunning", !isIdling);
            transform.GetComponent<Animator>().SetBool("isAttacking", !isIdling);
            transform.GetComponent<Animator>().SetBool("isWalking", !isIdling);
        }
    }

    public void IsAttacking(bool isAttacking)
    {
        if (transform.GetComponent<Animator>().GetBool("isAttacking") != true)
        {
            transform.GetComponent<Animator>().SetBool("isAttacking", isAttacking);
            transform.GetComponent<Animator>().SetBool("isRunning", !isAttacking);
            transform.GetComponent<Animator>().SetBool("isIdling", !isAttacking);
            transform.GetComponent<Animator>().SetBool("isWalking", !isAttacking);
        }
    }

    public void IsWalking(bool isWalking)
    {
        if (transform.GetComponent<Animator>().GetBool("isWalking") != true)
        {
            transform.GetComponent<Animator>().SetBool("isAttacking", !isWalking);
            transform.GetComponent<Animator>().SetBool("isRunning", !isWalking);
            transform.GetComponent<Animator>().SetBool("isIdling", !isWalking);
            transform.GetComponent<Animator>().SetBool("isWalking", isWalking);
        }
    }
}
