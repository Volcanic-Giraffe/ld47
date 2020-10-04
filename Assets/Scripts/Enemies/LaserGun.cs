using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public GameObject projectilePrefab;
    
    public Transform cannonPoint;
    public float chargeTime;
    
    private float _chargeTimer;
    private bool _charging;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_charging)
        {
            _chargeTimer -= Time.deltaTime;

            if (_chargeTimer < 0)
            {
                _chargeTimer = chargeTime;
                FireLaser();
            }
        }
    }

    void FireLaser()
    {
        Instantiate(projectilePrefab, cannonPoint.position, cannonPoint.rotation);
    }
    
    public void DischargeLaser()
    {
        _charging = false;
        _chargeTimer = chargeTime;
    }

    public void ChargeLaser()
    {
        _charging = true;
    }
}
