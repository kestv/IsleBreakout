using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    const int OFFENSIVE_SPELL = 1;
    const int TAME_SPELL = 2;
    const int RECALL_SPELL = 3;

    [SerializeField]
    GameObject castPoint;
    [SerializeField] 
    GameObject arrowCastPoint;
    float lastCast;
    GameObject currentPet;
    GameObject pet;
    bool triggering;

    SpellHolder slot1;
    SpellHolder slot2;

    GameObject castBar;
    GameObject bar;
    float startedCasting;
    float castTime = 2f;

    Coroutine coroutine;

    PlayerInventory playerInventory;
    [SerializeField]
    GameObject tameItem;
    AudioManager audio;

    Vector3 lastRecallPos = new Vector3(0, 0, 0);

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
        audio = GetComponent<AudioManager>();

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
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                castBar.SetActive(false);
                audio.Stop("Casting");
                GetComponent<PlayerMovementController>().enabled = true;
            }
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
            return slot1.GetComponent<SpellHolder>().GetSpell().GetComponent<SpellInfo>().name;
        }
        else if (slot == 2)
        {
            return slot2.GetComponent<SpellHolder>().GetSpell().GetComponent<SpellInfo>().name;
        }
        else return "";
    }

    public bool IsSlotTaken(int slot)
    {
        if (slot == 1)
        {
            if (slot1.GetComponent<SpellHolder>().GetSpell() != null)
                return true;
            else
                return false;
        }
        else if (slot == 2)
        {
            if (slot2.GetComponent<SpellHolder>().GetSpell() != null)
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

    public void TamePet(SpellHolder spellHolder, bool force = false)
    {
        var ui = UIHandler.Instance;
        var tameItem = this.tameItem.GetComponent<ItemSettings>();
        if (triggering && pet != null)
        {
            var item = playerInventory.ContainsTameItem();
            if (item != null)
            {
                spellHolder.SetOnCooldown(true);
                startedCasting = Time.time;
                bar.GetComponent<Image>().fillAmount = 0;
                castBar.SetActive(true);
                playerInventory.ConsumeItem(item.GetComponent<ItemSettings>().getItemID());
                StartCoroutine(IETamePet(ui, force));
                
            }
            else ui.DisplayMessage("You don't have food");
        }
        else ui.DisplayMessage("You are too far away");
    }

    public void Recall(SpellHolder spellHolder)
    {
        
        if(lastRecallPos.y != 0)
        {
            if (!GetComponent<PlayerCombatController>().GetInCombat())
            {
                StartCoroutine(IERecall(spellHolder));
            }
            else
            {
                UIHandler.Instance.DisplayMessage("Cannot do that while in combat");
            }
        }
        else
        {
            lastRecallPos = transform.position;
            UIHandler.Instance.DisplayMessage("Recall position set", true);
        }
    }

    public void CastSpell(GameObject target, SpellHolder spellHolder)
    {
        if (spellHolder.GetSpell() != null && !spellHolder.IsOnCooldown())
        {
            var type = spellHolder.GetSpell().GetComponent<SpellInfo>().type;
            switch (type)
            {
                case OFFENSIVE_SPELL:
                    if (target != null && Time.time - lastCast > spellHolder.GetSpell().GetComponent<SpellInfo>().cooldown && Vector3.Distance(transform.position, target.transform.position) <= 15)
                    {
                        coroutine = StartCoroutine(IECastingSpell(spellHolder, target));
                    }
                    else if (target == null)
                    {
                        UIHandler.Instance.DisplayMessage("Target something first");
                    }
                    break;
                case TAME_SPELL:
                    TamePet(spellHolder);
                    break;
                case RECALL_SPELL:
                    Recall(spellHolder);
                    break;
            }
        }
        else
        {
            UIHandler.Instance.DisplayMessage("Spell is on cooldown");
        }
    }

    IEnumerator IECastArrow(GameObject target, GameObject arrow)
    {
        GetComponent<PlayerMovementController>().enabled = false;
        yield return new WaitForSeconds(1f);
        var item = Instantiate(arrow, arrowCastPoint.transform.position, transform.rotation);
        item.transform.LookAt(target.transform);
        item.GetComponent<ProjectileMovement>().SetDamage(GetComponent<PlayerCombatController>().GetDamage() + GetComponent<PlayerStatsController>().GetStrength() * 2);
        item.GetComponent<ProjectileMovement>().SetTarget(target);
        GetComponent<PlayerMovementController>().enabled = true;
    }

    IEnumerator IEInstantiateSpell(GameObject target, GameObject spell)
    {
        GetComponent<PlayerMovementController>().enabled = false;
        GetComponent<Animator>().Play("SpellCast");
        yield return new WaitForSeconds(0.5f);
        audio.Stop("Casting");
        audio.Play("Release");
        var item = Instantiate(spell, castPoint.transform.position, transform.rotation);
        item.transform.LookAt(target.transform);
        item.GetComponent<ProjectileMovement>().SetDamage(item.GetComponent<ProjectileMovement>().GetDamage() + GetComponent<PlayerStatsController>().GetWisdom() * 5);
        item.GetComponent<ProjectileMovement>().SetTarget(target);
        GetComponent<PlayerMovementController>().enabled = true;
    }

    IEnumerator IECastingSpell(SpellHolder spellHolder, GameObject target)
    {
        GetComponent<PlayerMovementController>().enabled = false;
        bar.GetComponent<Image>().fillAmount = 0;
        castBar.SetActive(true);
        startedCasting = Time.time;
        audio.Play("Casting");
        yield return new WaitForSeconds(castTime);
        
        if (Time.time - startedCasting >= castTime)
        {
            GetComponent<PlayerCombatController>().SetInCombat(true);
            lastCast = Time.time;
            spellHolder.SetOnCooldown(true);
            if (spellHolder.GetImage() != null)
                spellHolder.GetImage().fillAmount = 0f;
            Debug.Log("Casting spell");
            StartCoroutine(IEInstantiateSpell(target, spellHolder.GetSpell()));
        }
    }

    IEnumerator IETamePet(UIHandler ui, bool force) //Force - to prevent test failures
    {
        audio.Play("Casting");
        yield return new WaitForSeconds(castTime);
        audio.Stop("Casting");
        if (Time.time - startedCasting >= castTime)
        {
            var chance = force ? 100 : Random.Range(0, 100); //For testing purposes
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

    IEnumerator IERecall(SpellHolder spellHolder)
    {
        spellHolder.SetOnCooldown(true);
        if (spellHolder.GetImage() != null)
            spellHolder.GetImage().fillAmount = 0f;
        GetComponent<PlayerMovementController>().enabled = false;
        GetComponent<PlayerCombatController>().enabled = false;
        GetComponent<Animator>().Play("Recall");
        yield return new WaitForSeconds(1f);
        transform.position = lastRecallPos;
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlayerMovementController>().enabled = true;
        GetComponent<PlayerCombatController>().enabled = true;

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

    //TESTS
    public Vector3 GetRecallPos()
    {
        return this.lastRecallPos;
    }

    public void AddItemForPet()
    {
        playerInventory.AddItem(Instantiate(tameItem));
    }
}
