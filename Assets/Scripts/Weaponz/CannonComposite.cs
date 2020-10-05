using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonComposite : Cannon
{
    public Cannon[] _cannons;

    public override void FireOnce()
    {
        foreach (var cannon in _cannons) cannon.FireOnce();
    }
    
    public override void SetUpgrades(TankUpgrades upgrades)
    {
        base.SetUpgrades(upgrades);

        Debug.Log("### I AM HITTING OVERRIDE!");
        
        foreach (var cannon in _cannons) cannon.SetUpgrades(upgrades);
    }

}
