using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public List<Transform> CannonPoint;
    public GameObject Projectile;
    public GameObject Flash;

    public float ShotSpeed;
    public float Firerate = 1f;

    float fireTimer = 0;
    int cpIndex = 0;
    
    private TankUpgrades _upgrades;

    public Transform NextCannonPoint
    {
        get
        {
            cpIndex += 1;
            if (cpIndex >= CannonPoint.Count) cpIndex = 0;
            return CannonPoint[cpIndex];

        }
    }

    void Start()
    {
        fireTimer = FirerateWithUpgrades();
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
            fireTimer = FirerateWithUpgrades();
            var proj = Instantiate(Projectile);
            var cp = NextCannonPoint;
            proj.transform.position = cp.position;
            proj.transform.rotation = transform.rotation;
            proj.GetComponent<Projectile>()?.SetUpgrades(_upgrades);
            proj.GetComponent<Rigidbody>().AddForce(transform.forward * ShotSpeed, ForceMode.VelocityChange);

            var flash = Instantiate(Flash, this.transform);
            flash.transform.GetChild(0).transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            flash.transform.position = cp.position;
        }

    }

    private float FirerateWithUpgrades()
    {
        if (_upgrades == null) return Firerate;
        return _upgrades.FirerateFormula(Firerate);
    }

    public void SetUpgrades(TankUpgrades upgrades)
    {
        _upgrades = upgrades;
    }
}
