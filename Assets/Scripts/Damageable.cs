﻿using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float Health = 20;
    
    [Range(0f, 1f)]
    public float AOECoefficient = 1f;

    public float MaxHealth = 20;

    public event Action OnDie;

    public event Action OnHit;

    public void Heal(float amount)
    {
        Health += amount;
        if (Health >= MaxHealth)
            Health = MaxHealth;
    }

    public void RestoreFullHealth()
    {
        Health = MaxHealth;
    }

    public void Damage(GameObject who, float amount, bool aoe = false)
    {
        OnHit?.Invoke();

        Debug.Log($"Damaging {gameObject.name} with {who.name} by {(aoe ? amount * AOECoefficient : amount)}.");
        Health -= aoe ? amount * AOECoefficient : amount;
        if (Health <= 0)
        {
            OnDie?.Invoke();
            Destroy(gameObject);
        }
    }
}
