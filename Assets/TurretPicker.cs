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

    void Start()
    {
        CurrentTurret = GetComponentInChildren<Turret>()?.gameObject;
        SetTurret(Selected);
    }

    void SetTurret(TurretKind kind)
    {
        Destroy(CurrentTurret);
        CurrentTurret = Instantiate(TurretPrefabs[(int)kind], TurretPoint.position, TurretPoint.rotation);
        CurrentTurret.transform.SetParent(transform);
    }
}
