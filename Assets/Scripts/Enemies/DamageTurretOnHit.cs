using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOT IN USE RIGHT NOW.
public class DamageTurretOnHit : MonoBehaviour
{
    public Damageable turret;
    
    private Damageable _damageable;
    
    void Start()
    {
        _damageable = GetComponent<Damageable>();
        _damageable.OnDie += KillTurret;
        _damageable.OnHit += DamageTurret;
    }

    private void KillTurret()
    {
        turret.Damage(gameObject, 100); // kill turret!
    }

    private void DamageTurret()
    {
        // may be?        
    }
    
}
