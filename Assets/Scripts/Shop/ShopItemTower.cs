using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemTower : ShopItem
{
    public TurretKind turret;
    
    public override void OnPurchased()
    {
        heroTurretPicker.SetTurret(turret);
    }
}
