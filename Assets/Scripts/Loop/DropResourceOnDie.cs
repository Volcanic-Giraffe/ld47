using System;
using Loop;
using UnityEngine;


[RequireComponent(typeof(Damageable))]
public class DropResourceOnDie : MonoBehaviour
{
    public GameObject resourcePrefab;
    public int drops = 1;
    
    private void Awake()
    {
        GetComponent<Damageable>().OnDie += DropResource;
    }

    private void DropResource()
    {
        for (int i = 0; i < drops; i++) Instantiate(resourcePrefab, transform.position, Quaternion.identity);
    }
}