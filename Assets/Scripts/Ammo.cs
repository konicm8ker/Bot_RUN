using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;

    // Return ammo amount
    public int RequestAmmoAmount()
    {
        return ammoAmount;
    }

    // Reduce ammo ammount
    public void ReduceAmmoAmount()
    {
        ammoAmount--;
    }
}
