using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretKind
{
    Basic, Big, Double, Rocket, MG
}
public class TurretPicker : MonoBehaviour
{
    public TurretKind Selected = TurretKind.Basic;
    public GameObject CurrentTurret;
    public Transform TurretPoint;
    public List<GameObject> TurretPrefabs = new List<GameObject>();

    private TankUpgrades _upgrades;
    
    void Start()
    {
        _upgrades = GetComponent<TankUpgrades>();
        
        CurrentTurret = GetComponentInChildren<Turret>()?.gameObject;
        SetTurret(Selected);
    }

    public void SetTurret(TurretKind kind)
    {
        Destroy(CurrentTurret);
        CurrentTurret = Instantiate(TurretPrefabs[(int)kind], TurretPoint.position, TurretPoint.rotation);
        CurrentTurret.GetComponent<Cannon>()?.SetUpgrades(_upgrades);
        CurrentTurret.transform.SetParent(transform);
    }
}
