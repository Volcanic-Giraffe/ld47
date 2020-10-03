using System;
using Loop;
using UnityEngine;


public class EnemySimple : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            GetComponent<SimpleGrowBehaviour>().ReplaceWithCorpse();
            Destroy(gameObject);
        }
    }
}