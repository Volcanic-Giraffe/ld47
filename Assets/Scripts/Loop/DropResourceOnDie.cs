using System;
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
        for (int i = 0; i < drops; i++)
        {
            var resource = Instantiate(resourcePrefab, transform.position, Quaternion.identity);
            var rndVec = Vector3.up + new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0, UnityEngine.Random.Range(-0.2f, 0.2f));
            resource.GetComponent<Rigidbody>().AddForce(rndVec * 6, ForceMode.VelocityChange);
        }
    }
}