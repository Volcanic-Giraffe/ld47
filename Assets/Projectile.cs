using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage = 5;
    public GameObject Explosion;

    void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.gameObject.GetComponent<Damageable>();
        if(damageable != null)
        {
            damageable.Damage(this.gameObject, Damage);
        }
        if (Explosion != null) Instantiate(Explosion, this.transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}
