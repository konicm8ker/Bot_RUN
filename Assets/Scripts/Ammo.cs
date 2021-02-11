using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    // Return ammo amount
    public int RequestAmmoAmount(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
        // return GetComponent<AmmoSlot>().ammoAmount;
    }

    // Reduce ammo ammount
    public void ReduceAmmoAmount(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
        // GetComponent<AmmoSlot>().ammoAmount--;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach(AmmoSlot slot in ammoSlots)
        {
            if(slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }
}
