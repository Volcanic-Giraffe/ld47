using System.Collections.Generic;
using UnityEngine;

public class DamageOnEnter : MonoBehaviour
{
    public float Damage = 5;
    HashSet<GameObject> alreadyDamaged = new HashSet<GameObject>();
    
    private TankUpgrades _upgrades;

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.attachedRigidbody?.gameObject?.GetComponent<Damageable>();
        
        if (damageable != null && !alreadyDamaged.Contains(damageable.gameObject))
        {
            alreadyDamaged.Add(damageable.gameObject);
            damageable.Damage(gameObject, DamageWithUpgrades(), true);
        }
    }

    private float DamageWithUpgrades()
    {
        if (_upgrades == null) return Damage;
        return _upgrades.DamageFormula(Damage);
    }
    
    public void SetUpgrades(TankUpgrades upgrades)
    {
        _upgrades = upgrades;
    }
}
