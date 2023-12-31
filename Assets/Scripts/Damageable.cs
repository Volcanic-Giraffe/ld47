﻿using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float Health = 20;
    
    [Range(0f, 1f)]
    public float AOECoefficient = 1f;

    public float MaxHealth = 20;

    public bool Invincible = false;

    public event Action OnDie;

    public event Action OnHit;

    public AudioClip[] soundsHit;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

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

        if (soundsHit != null && soundsHit.Length > 0)
        {
            AudioSource.PlayClipAtPoint(soundsHit[UnityEngine.Random.Range(0, soundsHit.Length)], Camera.main.transform.position, 0.5f);
        }

        if (Invincible)
        {
            Debug.Log($" {gameObject.name} INVINCIBLE");
            return;
        }
        Debug.Log($"Damaging {gameObject.name} with {who.name} by {(aoe ? amount * AOECoefficient : amount)}.");
        Health -= aoe ? amount * AOECoefficient : amount;
        if (Health <= 0)
        {
            OnDie?.Invoke();
            Destroy(gameObject);
        }
    }

    public bool IsAlive()
    {
        return Health > 0;
    }
}
