using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject aimPrefab;

    public Transform cannonPoint;
    public float chargeTime;
    
    private float _chargeTimer;
    public bool Charging;
    private GameObject aimBeam;

    void Start()
    {
    }

    void FireLaser()
    {
        if (aimBeam != null) Destroy(aimBeam);
        if (Charging == false) return;
        Instantiate(projectilePrefab, cannonPoint.position, cannonPoint.rotation);
    }
    
    public void DischargeLaser()
    {
        Charging = false;
        _chargeTimer = chargeTime;
    }

    public void ChargeLaser()
    {
        Charging = true;
        aimBeam = Instantiate(aimPrefab, cannonPoint.position, cannonPoint.rotation);
        aimBeam.transform.SetParent(this.transform);
    }

    public IEnumerator WaitForChargeAndFire()
    {
        ChargeLaser();
        yield return new WaitForSeconds(chargeTime);
        FireLaser();
        DischargeLaser();
    }
}
