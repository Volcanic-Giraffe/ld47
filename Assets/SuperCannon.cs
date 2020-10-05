using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCannon : MonoBehaviour
{
    public GameObject Projectile;
    public int Charges = 0;
    public float ChargeTime = 30;
    public Transform cannonPoint;
    float chargeTimer = 10;
    public AudioClip Sound;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        chargeTimer -= Time.deltaTime;
        if (chargeTimer <= 0)
        {
            chargeTimer = ChargeTime;
            if (Charges < 2) Charges += 1;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (Charges == 0) return;
        Charges--;
        if (Sound != null) AudioSource.PlayClipAtPoint(Sound, Camera.main.transform.position, 0.3f);

        var proj = Instantiate(Projectile);
        proj.transform.position = cannonPoint.position;
        proj.transform.rotation = transform.rotation;
        proj.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up*0.6f) * 8, ForceMode.VelocityChange);
    }
}
