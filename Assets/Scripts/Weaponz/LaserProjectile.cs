using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : Projectile
{
    private const float MaxDistance = 20f;

    public Collider mainCollider;

    private float ttl = 1f; // время затухания
    private float ttlTimer;
    
    void Start()
    {
        ttlTimer = 0;
        EnlargeLaser();
    }

    private void Update()
    {
        if (ttlTimer > 0) ttlTimer -= Time.deltaTime;
        if (ttlTimer < 0)
        {
            base.DestroyProjectile();
        }
        if (ttlTimer > 0)
        {
            var scaleXY = transform.localScale.x; // the same as Y
            var reduced = ttlTimer / ttl * scaleXY;
            transform.localScale = new Vector3(reduced, reduced, transform.localScale.z);
        }
    }

    void EnlargeLaser()
    {
        Ray ray = new Ray(transform.position, (transform.forward).normalized * MaxDistance);
        RaycastHit hit;

        int mask = LayerMask.GetMask("Wall", "Default");

        if (Physics.Raycast(ray, out hit, MaxDistance, mask))
        {
            var size = hit.distance;

            var scaleChange = Vector3.forward * size;

            transform.localScale += scaleChange;
        }
    }

    protected override void DoDamage(GameObject target)
    {
        mainCollider.enabled = false;
        
        base.DoDamage(target);
    }

    protected override void DestroyProjectile()
    {
        ttlTimer = ttl;
    }
}
