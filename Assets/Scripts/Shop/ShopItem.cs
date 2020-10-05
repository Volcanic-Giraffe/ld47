using System;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int price;
    public string description;

    public Transform hoverGood;
    public Transform hoverBad;

    public GameObject purchaseEffect;
    
    private void Start()
    {
        OnBlur();
    }

    public bool TryPurchase()
    {
        // todo: price check
        var enoughMoney = true;

        if (enoughMoney)
        {
            OnPurchased();
        
            if (purchaseEffect != null) Instantiate(purchaseEffect, transform.position, Quaternion.identity);
        
            Destroy(gameObject);
        
            return true;
        }

        return false;
    }

    public virtual void OnPurchased()
    {
        // override!
    }

    public virtual void OnHover()
    {
        var hoverEffect = hoverGood; // todo: or bad, if low money

        if (hoverEffect != null) hoverEffect.gameObject.SetActive(true);
    }

    public virtual void OnBlur()
    {
        if (hoverBad != null) hoverBad.gameObject.SetActive(false);
        if (hoverGood != null) hoverGood.gameObject.SetActive(false);
    }
}