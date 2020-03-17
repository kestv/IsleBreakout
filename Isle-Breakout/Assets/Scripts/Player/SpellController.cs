using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public float range;
    public GameObject castPoint;
    float lastCast;
    void Start()
    {
        lastCast = 0;
    }

    void Update()
    {
    }

    public void castSpell(GameObject target, SpellHolder spellHolder)
    {
        if (Time.time - lastCast > spellHolder.spell.GetComponent<ProjectileMoveScript>().cooldown && Vector3.Distance(transform.position, target.transform.position) <= 15)
        {
            lastCast = Time.time;
            //GetComponent<Animator>().Play("SpellCast");
            spellHolder.cooldown = true;
            spellHolder.image.fillAmount = 0f;
            StartCoroutine(waitCoroutine(target, spellHolder.spell));
            //var item = Instantiate(spell, castPoint.transform.position, Quaternion.identity);
            //item.GetComponent<ProjectileMoveScript>().target = target;
            //lastCast = Time.time;
        }

    }

    IEnumerator waitCoroutine(GameObject target, GameObject spell)
    {
        GetComponent<PlayerMovementController>().enabled = false;
        GetComponent<Animator>().Play("SpellCast");
        yield return new WaitForSeconds(0.5f);
        var item = Instantiate(spell, castPoint.transform.position, Quaternion.identity);
        item.GetComponent<ProjectileMoveScript>().actualDamage = item.GetComponent<ProjectileMoveScript>().damage + GetComponent<PlayerStatsController>().wisdom * 5;
        item.GetComponent<ProjectileMoveScript>().target = target;
        GetComponent<PlayerMovementController>().enabled = true;
    }
}
