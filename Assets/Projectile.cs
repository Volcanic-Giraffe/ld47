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
        var damageable = collision.gameObject.GetComponent<Damageable>();

        bool validTag = string.IsNullOrEmpty(hitOnlyTag) || collision.gameObject.CompareTag(hitOnlyTag);
        
        if (damageable != null && validTag && !hitOnce)
        {
            hitOnce = true;
            damageable.Damage(this.gameObject, Damage);
        }
        if (Explosion != null && validTag) Instantiate(Explosion, this.transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
