using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotPanelController : MonoBehaviour
{
    public Transform helmetSlot;
    public Transform torsoSlot;
    public Transform legsSlot;
    public Transform shouldersSlot;
    public Transform capeSlot;
    public Transform primaryWeaponSlot;
    public Transform secondaryWeaponSlot;

    private void Start()
    {
        helmetSlot          = transform.GetChild(0);
        torsoSlot           = transform.GetChild(1);
        legsSlot            = transform.GetChild(2);
        shouldersSlot       = transform.GetChild(3);
        capeSlot            = transform.GetChild(4);
        primaryWeaponSlot   = transform.GetChild(5);
        secondaryWeaponSlot = transform.GetChild(6);
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
            //case "Helmet":
            //    return helmetSlot;
            //    break;
            //case "Helmet":
            //    return helmetSlot;
            //    break;
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

    public Transform getPrimaryWeaponSlot()
    { return primaryWeaponSlot; }

    public Transform getSecondaryWeaponSlot()
    { return secondaryWeaponSlot; }
}
