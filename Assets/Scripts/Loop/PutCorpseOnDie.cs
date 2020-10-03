using System;
using Loop;
using UnityEngine;


[RequireComponent(typeof(Damageable), typeof(SimpleGrowBehaviour))]
public class PutCorpseOnDie : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Damageable>().OnDie += OnOnDie;
    }

    private void OnOnDie()
    {
        GetComponent<SimpleGrowBehaviour>().ReplaceWithCorpse();
    }
}