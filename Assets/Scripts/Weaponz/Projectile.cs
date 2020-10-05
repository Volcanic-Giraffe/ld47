using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage = 5;
    public GameObject Explosion;

    public string hitOnlyTag;

    private bool hitOnce;
    private TankUpgrades _upgrades;

    void OnCollisionEnter(Collision collision)
    {
        DoDamage(collision.gameObject);
    }

    protected virtual void DoDamage(GameObject target)
    {
        var damageable = target.GetComponent<Damageable>();

        bool validTag = string.IsNullOrEmpty(hitOnlyTag) || target.CompareTag(hitOnlyTag);
        
        if (damageable != null && validTag && !hitOnce)
        {
            hitOnce = true;
            damageable.Damage(this.gameObject, DamageWithUpgrades());
        }

        if (Explosion != null && validTag)
        {
            var explode = Instantiate(Explosion, this.transform.position, Quaternion.identity);
            explode.GetComponent<DamageOnEnter>()?.SetUpgrades(_upgrades);
        }
        
        DestroyProjectile();
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(gameObject);
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
