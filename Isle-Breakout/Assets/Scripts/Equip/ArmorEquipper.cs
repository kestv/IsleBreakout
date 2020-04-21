using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEquipper : MonoBehaviour
{
    private DependencyManager manager;

    [Header("Attack refrences")]
    [SerializeField] private Transform playerModel;   //Modular_Characters
    [SerializeField] private bool isRenderer;         //Is the script on a player renderer

    [Header("HEAD")]
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject hair;
    [SerializeField] private GameObject mask;
    [SerializeField] private GameObject helmet;

    [Header("CAPE")]
    [SerializeField] private GameObject cape;

    [Header("SHOULDERS")]
    [SerializeField] private GameObject shoulderRight;
    [SerializeField] private GameObject shoulderLeft;

    [Header("TORSO")]
    [SerializeField] private GameObject torso;
    [SerializeField] private GameObject armUpperRight;
    [SerializeField] private GameObject armUpperLeft;
    [SerializeField] private GameObject armLowerRight;
    [SerializeField] private GameObject armLowerLeft;
    [SerializeField] private GameObject handRight;
    [SerializeField] private GameObject handLeft;

    [Header("LEGS")]
    [SerializeField] private GameObject hips;
    [SerializeField] private GameObject legRight;
    [SerializeField] private GameObject legLeft;

    [Header("MELEE WEAPON")]
    [SerializeField] private GameObject meleeWeapon;

    [Header("RANGED WEAPON")]
    [SerializeField] private GameObject rangedWeapon;

    //Default TORSO meshes
    private Mesh defaultTorso;
    private Mesh defaultArmUpperRight;
    private Mesh defaultArmUpperLeft;
    private Mesh defaultArmLowerRight;
    private Mesh defaultArmLowerLeft;
    private Mesh defaultHandRight;
    private Mesh defaultHandLeft;

    //Default LEGS meshes
    private Mesh defaultHips;
    private Mesh defaultLegRight;
    private Mesh defaultLegLeft;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
        InitDefaultMeshes();
    }

    public void InitDefaultMeshes()
    {
        //Torso
        defaultTorso = GetObjectMesh(torso);
        defaultArmUpperRight = GetObjectMesh(armUpperRight);
        defaultArmUpperLeft = GetObjectMesh(armUpperLeft);
        defaultArmLowerRight = GetObjectMesh(armLowerRight);
        defaultArmLowerLeft = GetObjectMesh(armLowerLeft);
        defaultHandRight = GetObjectMesh(handRight);
        defaultHandLeft = GetObjectMesh(handLeft);
        //Legs
        defaultHips = GetObjectMesh(hips);
        defaultLegRight = GetObjectMesh(legRight);
        defaultLegLeft = GetObjectMesh(legLeft);
    }

    public Mesh GetObjectMesh(GameObject go)
    {
        return go.GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    public void SetObjectMesh(GameObject go, Mesh mesh)
    {
        go.GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
    }

    public void EditStats(IArmor equipment, bool increase)
    {
        PlayerStatsController ctrl = transform.root.GetComponent<PlayerStatsController>();
        PlayerStatsPanelController panelCtrl = manager.getEquipPanel().transform.GetChild(0).GetChild(3).GetComponent<PlayerStatsPanelController>();

        int modifier = 1;
        if (!increase)
        {
            modifier *= -1;
        }

        ctrl.updateStrength(equipment.getStrength() * modifier);
        ctrl.updateSpeed(equipment.getSpeed() * modifier);
        ctrl.updateWisdom(equipment.getWisdom() * modifier);
        ctrl.updateHP(equipment.getHP() * modifier);

        panelCtrl.UpdateStats();
    }

    public void Equip(IArmor equipment)
    {
        List<Mesh> meshes = equipment.getMeshes();

        switch (equipment)
        {
            case Mask mask:
                EquipMask(meshes);
                break;
            case Helmet helmet:
                EquipHelmet(meshes);
                break;
            case Cape cape:
                EquipCape(meshes);
                break;
            case Shoulders shoulders:
                EquipShoulders(meshes);
                break;
            case Torso torso:
                EquipTorso(meshes);
                break;
            case Legs legs:
                EquipLegs(meshes);
                break;
            case MeleeWeapon meleeWeapon:
                EquipMeleeWeapon(meshes);
                break;
            case RangedWeapon rangedWeapon:
                EquipRangedWeapon(meshes);
                break;
            default:
                break;
        }

        if (!isRenderer)
        {
            EditStats(equipment, true);
        }
        else
        {
            PlayerStatsPanelController panelCtrl = manager.getEquipPanel().transform.GetChild(0).GetChild(3).GetComponent<PlayerStatsPanelController>();
            panelCtrl.UpdateStats();
        }
    }

    public void Unequip(IArmor equipment)
    {
        switch (equipment)
        {
            case Mask mask:
                UnequipMask();
                break;
            case Helmet helmet:
                UnequipHelmet();
                break;
            case Cape cape:
                UnequipCape();
                break;
            case Shoulders shoulders:
                UnequipShoulders();
                break;
            case Torso torso:
                UnequipTorso();
                break;
            case Legs legs:
                UnequipLegs();
                break;
            case MeleeWeapon meleeWeapon:
                UnequipMeleeWeapon();
                break;
            case RangedWeapon rangedWeapon:
                UnequipRangedWeapon();
                break;
            default:
                break;
        }

        if (!isRenderer)
        {
            EditStats(equipment, false);
        }
        else
        {
            PlayerStatsPanelController panelCtrl = manager.getEquipPanel().transform.GetChild(0).GetChild(3).GetComponent<PlayerStatsPanelController>();
            panelCtrl.UpdateStats();
        }
    }

    public void EquipItem(GameObject target, Mesh mesh)
    {
        target.SetActive(true);
        SetObjectMesh(target, mesh);
    }

    public void UnequipItem(GameObject target)
    {
        target.SetActive(false);
    }

    public void UnequipItem(GameObject target, Mesh defaultMesh)
    {
        SetObjectMesh(target, defaultMesh);
    }

    public void EquipWeapon(GameObject target, Mesh mesh)
    {
        target.GetComponent<MeshFilter>().mesh = mesh;
    }

    public void UnequipWeapon(GameObject target)
    {
        target.GetComponent<MeshFilter>().mesh = null;
    }

    public void EquipMask(List<Mesh> meshes)
    {
        head.SetActive(false);
        hair.SetActive(false);
        EquipItem(mask, meshes[0]);
    }

    public void UnequipMask()
    {
        head.SetActive(true);
        hair.SetActive(true);
        UnequipItem(mask);
    }

    public void EquipHelmet(List<Mesh> meshes)
    {
        hair.SetActive(false);
        EquipItem(helmet, meshes[0]);
    }

    public void UnequipHelmet()
    {
        hair.SetActive(true);
        UnequipItem(helmet);
    }

    public void EquipCape(List<Mesh> meshes)
    {
        EquipItem(cape, meshes[0]);
    }

    public void UnequipCape()
    {
        UnequipItem(cape);
    }

    public void EquipShoulders(List<Mesh> meshes)
    {
        EquipItem(shoulderRight, meshes[0]);
        EquipItem(shoulderLeft, meshes[1]);
    }

    public void UnequipShoulders()
    {
        UnequipItem(shoulderRight);
        UnequipItem(shoulderLeft);
    }

    public void EquipTorso(List<Mesh> meshes)
    {
        EquipItem(torso, meshes[0]);
        EquipItem(armUpperRight, meshes[1]);
        EquipItem(armUpperLeft, meshes[2]);
        EquipItem(armLowerRight, meshes[3]);
        EquipItem(armLowerLeft, meshes[4]);
        EquipItem(handRight, meshes[5]);
        EquipItem(handLeft, meshes[6]);
    }

    public void UnequipTorso()
    {
        UnequipItem(torso, defaultTorso);
        UnequipItem(armUpperRight, defaultArmUpperRight);
        UnequipItem(armUpperLeft, defaultArmUpperLeft);
        UnequipItem(armLowerRight, defaultArmLowerRight);
        UnequipItem(armLowerLeft, defaultArmLowerLeft);
        UnequipItem(handRight, defaultHandRight);
        UnequipItem(handLeft, defaultHandLeft);
    }

    public void EquipLegs(List<Mesh> meshes)
    {
        EquipItem(hips, meshes[0]);
        EquipItem(legRight, meshes[1]);
        EquipItem(legLeft, meshes[2]);
    }

    public void UnequipLegs()
    {
        UnequipItem(hips, defaultHips);
        UnequipItem(legRight, defaultLegRight);
        UnequipItem(legLeft, defaultLegLeft);
    }

    public void EquipMeleeWeapon(List<Mesh> meshes)
    {
        EquipWeapon(meleeWeapon, meshes[0]);
    }

    public void UnequipMeleeWeapon()
    {
        UnequipWeapon(meleeWeapon);
    }

    public void EquipRangedWeapon(List<Mesh> meshes)
    {
        EquipItem(rangedWeapon, meshes[0]);

        if (!isRenderer)
        {
            GetComponent<PlayerCombatController>().setIsRangedWeaponEquipped(true);
        }        
    }

    public void UnequipRangedWeapon()
    {
        UnequipItem(rangedWeapon, null);

        if (!isRenderer)
        {
            GetComponent<PlayerCombatController>().setIsRangedWeaponEquipped(false);
        }
    }
}
