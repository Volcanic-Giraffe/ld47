using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpgrades : MonoBehaviour
{
    // Multiplicative (so 0.9 is ~-10%, 0.8 is ~-20%)
    public float firerateBuff;
    
    // Additive, so +1 is +1 damage
    public int powerBuff;
    
    // Change in editor for dev tests, must be 0 in release
    public int firerateUpgrades;
    public int powerUpgrades;
    public int specialUpgrades;

    
    // affects projectiles and explosions!
    public float DamageFormula(float damage)
    {
        return damage + (powerUpgrades * powerBuff); // imba for smg, meh for BigGun
    }

    public float FirerateFormula(float firerate)
    {
        if (firerateUpgrades == 0) return firerate;
        return firerate * (Mathf.Pow(firerateBuff, firerateUpgrades));
    }
}
