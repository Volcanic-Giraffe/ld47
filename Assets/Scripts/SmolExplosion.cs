using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SmolExplosion : MonoBehaviour
{
    public AudioClip impactSound;

    private void Start()
    {
        if (impactSound != null) AudioSource.PlayClipAtPoint(impactSound, transform.position);
    }

    public void Die(float theValue)
    {
        Destroy(gameObject);
    }
}
