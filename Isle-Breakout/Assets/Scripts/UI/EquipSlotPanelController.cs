using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotPanelController : MonoBehaviour
{
    private Transform helmetSlot;
    private Transform torsoSlot;
    private Transform legsSlot;
    private Transform shouldersSlot;
    private Transform capeSlot;
    private Transform meleeWeaponSlot;
    private Transform rangedWeaponSlot;

    private void Start()
    {
        helmetSlot          = transform.GetChild(0);
        torsoSlot           = transform.GetChild(1);
        legsSlot            = transform.GetChild(2);
        shouldersSlot       = transform.GetChild(3);
        capeSlot            = transform.GetChild(4);
        meleeWeaponSlot     = transform.GetChild(5);
        rangedWeaponSlot    = transform.GetChild(6);
    }

    public Transform getSlot(string slotType)
    {
        switch (slotType)
        {
            case "Helmet":
                return helmetSlot;
            case "Torso":
                return torsoSlot;
            case "Legs":
                return legsSlot;
            case "Shoulders":
                return shouldersSlot;
            case "Cape":
                return capeSlot;
            case "MeleeWeapon":
                return meleeWeaponSlot;
            case "RangedWeapon":
                return rangedWeaponSlot;
            default:
                break;
        }

        return null;
    }

    public Transform getHelmetSlot()
    { return helmetSlot; }

    public Transform getTorsoSlot()
    { return torsoSlot; }

    public Transform getlegsSlot()
    { return legsSlot; }

    public Transform getShouldersSlot()
    { return shouldersSlot; }

    public Transform getCapeSlot()
    { return capeSlot; }

    public Transform getMeleeWeaponSlot()
    { return meleeWeaponSlot; }

    public Transform getRangedWeaponSlot()
    { return rangedWeaponSlot; }
}
