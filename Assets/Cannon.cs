using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform CannonPoint;
    public GameObject Projectile;

    public float ShotSpeed;
    public float Firerate = 1f;

    float fireTimer = 0;

    void Start()
    {
        fireTimer = Firerate;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        if (fireTimer <= 0)
        {
            fireTimer = Firerate;
            var proj = Instantiate(Projectile);
            proj.transform.position = CannonPoint.position;
            proj.transform.rotation = transform.rotation;
            proj.GetComponent<Rigidbody>().AddForce(transform.forward * ShotSpeed, ForceMode.VelocityChange);
        }

    }
}
