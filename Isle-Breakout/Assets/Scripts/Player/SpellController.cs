﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    public const int OFFENSIVE_SPELL = 1;
    public const int NEUTRAL_SPELL = 2;

    public float range;
    public GameObject castPoint;
    [SerializeField] private GameObject arrowCastPoint;
    float lastCast;
    GameObject currentPet;
    GameObject pet;
    bool triggering;

    SpellHolder slot1;
    SpellHolder slot2;

    public GameObject castBar;
    public GameObject bar;
    float startedCasting;
    float castTime = 2f;

    Coroutine coroutine;

    PlayerInventory playerInventory;
    public GameObject tameItem;
    void Start()
    {
        castBar = GameObject.Find("CastBar");
        bar = GameObject.Find("Bar");
        startedCasting = 0;
        castBar.SetActive(false);
        triggering = false;
        lastCast = 0;
        slot1 = GameObject.Find("Slot1").GetComponent<SpellHolder>();
        slot2 = GameObject.Find("Slot2").GetComponent<SpellHolder>();
        castPoint = GameObject.Find("SpellCast");

        playerInventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (Time.time - startedCasting <= castTime)
        {
            bar.GetComponent<Image>().fillAmount += 1f / castTime * Time.deltaTime;
        }
        else if(castBar.activeSelf == true)
        { 
            castBar.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StopCoroutine(coroutine);
            castBar.SetActive(false);
        }
    }

    public void SetSpell(GameObject spell, int slot)
    {
        if (slot == 1)
        {
            slot1.GetComponent<SpellHolder>().SetSpell(spell);
        }
        else if (slot == 2)
        {
            slot2.GetComponent<SpellHolder>().SetSpell(spell);
        }
    }

    public string GetSpellName(int slot)
    {
        if (slot == 1)
        {
            return slot1.GetComponent<SpellHolder>().spell.GetComponent<SpellInfo>().name;
        }
        else if (slot == 2)
        {
            return slot2.GetComponent<SpellHolder>().spell.GetComponent<SpellInfo>().name;
        }
        else return "";
    }

    public bool IsSlotTaken(int slot)
    {
        if (slot == 1)
        {
            if (slot1.GetComponent<SpellHolder>().spell != null)
                return true;
            else
                return false;
        }
        else if (slot == 2)
        {
            if (slot2.GetComponent<SpellHolder>().spell != null)
                return true;
            else
                return false;
        }
        return false;
    }

    public void CastArrow(GameObject target, GameObject arrow)
    {
        StartCoroutine(IECastArrow(target, arrow));
    }

    public void TamePet(SpellHolder spellHolder)
    {
        var ui = UIEventHandler.Instance;
        var tameItem = this.tameItem.GetComponent<ItemSettings>();
        if (triggering && pet != null)
        {
            if (playerInventory.Contains(tameItem.getItemID()))
            {
                startedCasting = Time.time;
                bar.GetComponent<Image>().fillAmount = 0;
                castBar.SetActive(true);
                playerInventory.ConsumeItem(tameItem.getItemID());
                StartCoroutine(TamePet(ui));
                
            }
            else ui.DisplayMessage("You don't have food");
        }
        else ui.DisplayMessage("You are too far away");
    }

    public void CastSpell(GameObject target, SpellHolder spellHolder)
    {
        if (spellHolder.spell != null)
        {
            var type = spellHolder.spell.GetComponent<SpellInfo>().type;
            switch (type)
            {
                case OFFENSIVE_SPELL:
                    if (target != null && Time.time - lastCast > spellHolder.spell.GetComponent<SpellInfo>().cooldown && Vector3.Distance(transform.position, target.transform.position) <= 15)
                    {
                        coroutine = StartCoroutine(CastingSpell(spellHolder, target));
                    }
                    else if (target == null)
                    {
                        UIEventHandler.Instance.DisplayMessage("Target something first");
                    }
                    break;
                case NEUTRAL_SPELL:
                    TamePet(spellHolder);
                    break;
            }
        }
    }

    IEnumerator IECastArrow(GameObject target, GameObject arrow)
    {
        GetComponent<PlayerMovementController>().enabled = false;
        yield return new WaitForSeconds(1f);
        var item = Instantiate(arrow, arrowCastPoint.transform.position, transform.rotation);
        item.transform.LookAt(target.transform);
        item.GetComponent<ProjectileMoveScript>().actualDamage = item.GetComponent<ProjectileMoveScript>().damage + GetComponent<PlayerStatsController>().wisdom * 5;
        item.GetComponent<ProjectileMoveScript>().target = target;
        GetComponent<PlayerMovementController>().enabled = true;
    }

    IEnumerator InstantiateSpell(GameObject target, GameObject spell)
    {
        GetComponent<PlayerMovementController>().enabled = false;
        GetComponent<Animator>().Play("SpellCast");
        yield return new WaitForSeconds(0.5f);
        var item = Instantiate(spell, castPoint.transform.position, transform.rotation);
        item.transform.LookAt(target.transform);
        item.GetComponent<ProjectileMoveScript>().actualDamage = item.GetComponent<ProjectileMoveScript>().damage + GetComponent<PlayerStatsController>().wisdom * 5;
        item.GetComponent<ProjectileMoveScript>().target = target;
        GetComponent<PlayerMovementController>().enabled = true;
    }

    IEnumerator CastingSpell(SpellHolder spellHolder, GameObject target)
    {
        GetComponent<PlayerMovementController>().enabled = false;
        bar.GetComponent<Image>().fillAmount = 0;
        castBar.SetActive(true);
        startedCasting = Time.time;
        yield return new WaitForSeconds(castTime);
        if (Time.time - startedCasting >= castTime)
        {
            GetComponent<PlayerCombatController>().inCombat = true;
            lastCast = Time.time;
            spellHolder.cooldown = true;
            if (spellHolder.image != null)
                spellHolder.image.fillAmount = 0f;
            Debug.Log("Casting spell");
            StartCoroutine(InstantiateSpell(target, spellHolder.spell));
        }
    }

    IEnumerator TamePet(UIEventHandler ui)
    {
        yield return new WaitForSeconds(castTime);
        if (Time.time - startedCasting >= castTime)
        {
            var chance = Random.Range(0, 100);
            if (chance >= 50)
            {
                if (currentPet != null)
                    currentPet.GetComponent<PetController>().SetUntamed();
                pet.GetComponent<PetController>().SetTamed();
                currentPet = pet;
                ui.DisplayMessage("Pet tamed successfully");
            }
            else
            {
                ui.DisplayMessage("Pet got away");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pet")
        {
            pet = other.gameObject;
            triggering = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pet")
        {
            pet = null;
            triggering = false;
        }
    }
}
