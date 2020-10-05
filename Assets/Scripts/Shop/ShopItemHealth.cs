using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemHealth : ShopItem
{
    public override void OnPurchased()
    {
        heroHealth.Heal(5);
    }
}
