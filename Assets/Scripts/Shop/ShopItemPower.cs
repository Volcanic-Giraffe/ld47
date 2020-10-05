using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPower : ShopItem
{
    public override void OnPurchased()
    {
        heroUpgrades.powerUpgrades += 1;
    }
}
