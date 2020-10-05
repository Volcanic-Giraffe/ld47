using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonComposite : Cannon
{
    public Cannon[] _cannons;

    public override void FireOnce()
    {
        if (_audio != null) _audio.Play();
        foreach (var cannon in _cannons) cannon.FireOnce();
    }
    
    public override void SetUpgrades(TankUpgrades upgrades)
    {
        base.SetUpgrades(upgrades);

        foreach (var cannon in _cannons) cannon.SetUpgrades(upgrades);
    }

}
