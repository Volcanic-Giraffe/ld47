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
            damageable.Damage(this.gameObject, Damage);
        }
        if (Explosion != null && validTag) Instantiate(Explosion, this.transform.position, Quaternion.identity);
        
        DestroyProjectile();
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
