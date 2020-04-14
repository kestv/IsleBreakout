using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEquipper : MonoBehaviour
{ 
    public Transform playerModel;   //Modular_Characters

    [Header("HEAD")]
    public GameObject hair;
    public GameObject mask;
    public GameObject helmet;

    [Header("CAPE")]
    public GameObject cape;

    [Header("SHOULDERS")]
    public GameObject shoulderRight;
    public GameObject shoulderLeft;

    [Header("TORSO")]
    public GameObject torso;
    public GameObject armUpperRight;
    public GameObject armUpperLeft;
    public GameObject armLowerRight;
    public GameObject armLowerLeft;
    public GameObject handRight;
    public GameObject handLeft;

    [Header("LEGS")]
    public GameObject hips;
    public GameObject legRight;
    public GameObject legLeft;

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

    public Helmet helm;
    public Torso tors;
    public Legs leg;
    public Cape cap;
    public Shoulders should;

    private void Start()
    {
        InitDefaultMeshes();
        //Equip(helm);
        //Equip(tors);
        //Equip(leg);
        //Equip(cap);
        //Equip(should);
    }

    public void EQP()
    {
        Debug.Log("EQUIPPED");
        Equip(helm);
        Equip(tors);
        Equip(leg);
        Equip(cap);
        Equip(should);
    }

    public void UNEQP()
    {
        Debug.Log("UNEQUIPPED");
        Unequip(helm);
        Unequip(tors);
        Unequip(leg);
        Unequip(cap);
        Unequip(should);
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

    //TODO CLEAN UP METHODS
    public void AddStats(IArmor equipment)
    {
        PlayerStatsController ctrl = transform.root.GetComponent<PlayerStatsController>();
        PlayerStatsPanelController panelCtrl = GameObject.Find("Manager").GetComponent<DependencyManager>().getEquipPanel().transform.GetChild(0).GetChild(3).GetComponent<PlayerStatsPanelController>();

        if(ctrl != null)
        {
            ctrl.updateStrength(equipment.getStrength());
            ctrl.updateSpeed(equipment.getSpeed());
            ctrl.updateWisdom(equipment.getWisdom());

            panelCtrl.UpdateStats();
        }
    }

    public void RemoveStats(IArmor equipment)
    {
        PlayerStatsController ctrl = transform.root.GetComponent<PlayerStatsController>();
        PlayerStatsPanelController panelCtrl = GameObject.Find("Manager").GetComponent<DependencyManager>().getEquipPanel().transform.GetChild(0).GetChild(3).GetComponent<PlayerStatsPanelController>();

        if (ctrl != null)
        {
            ctrl.updateStrength(equipment.getStrength() * -1);
            ctrl.updateSpeed(equipment.getSpeed() * -1);
            ctrl.updateWisdom(equipment.getWisdom() * -1);

            panelCtrl.UpdateStats();
        }
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
            default:
                break;
        }

        AddStats(equipment);
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
            default:
                break;
        }

        RemoveStats(equipment);
    }

    public void EquipItem(GameObject target, Mesh mesh)
    {
        target.SetActive(true);
        target.GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
    }

    public void UnequipItem(GameObject target)
    {
        target.SetActive(false);
    }

    public void UnequipItem(GameObject target, Mesh defaultMesh)
    {
        SetObjectMesh(target, defaultMesh);
    }

    public void EquipMask(List<Mesh> meshes)
    {
        EquipItem(mask, meshes[0]);
    }

    public void UnequipMask()
    {
        UnequipItem(mask);
    }

    public void EquipHelmet(List<Mesh> meshes)
    {
        hair.SetActive(false);
        EquipItem(helmet, meshes[0]);
    }

    public void UnequipHelmet()
    {
        hair.gameObject.SetActive(true);
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
}
