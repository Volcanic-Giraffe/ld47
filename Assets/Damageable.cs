using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float Health = 20;

    public event Action OnDie;

    public event Action OnHit;
    
    public void Damage(GameObject who, float amount)
    {
        OnHit?.Invoke();
        
        Debug.Log($"Damaging {gameObject.name} with {who.name} by {amount}.");
        Health -= amount;
        if (Health <= 0)
        {
            OnDie?.Invoke();
            Destroy(gameObject);
        }
    }
}
