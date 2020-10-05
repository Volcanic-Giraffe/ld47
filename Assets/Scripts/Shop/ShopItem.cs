using System;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int price;
    public string description;

    public Transform hoverGood;
    public Transform hoverBad;

    public GameObject purchaseEffect;
    
    protected GameObject hero;
    protected Damageable heroHealth;
    protected TurretPicker heroTurretPicker;
    protected TankUpgrades heroUpgrades;

    private void Start()
    {
        OnBlur();
        
        hero = GameObject.FindGameObjectWithTag("Hero");
        heroHealth = hero.GetComponent<Damageable>();
        heroTurretPicker = hero.GetComponent<TurretPicker>();
        heroUpgrades = hero.GetComponent<TankUpgrades>();
    }

    public bool TryPurchase()
    {
        if (IsEnoughMoney())
        {
            GameState.GameState.GetInstance().Resources -= price;
            
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
        if (hoverBad != null) hoverBad.gameObject.SetActive(!IsEnoughMoney());
        if (hoverGood != null) hoverGood.gameObject.SetActive(IsEnoughMoney());
    }

    public virtual void OnBlur()
    {
        if (hoverBad != null) hoverBad.gameObject.SetActive(false);
        if (hoverGood != null) hoverGood.gameObject.SetActive(false);
    }
    
    private bool IsEnoughMoney()
    {
        return GameState.GameState.GetInstance().Resources >= price;
    }
}