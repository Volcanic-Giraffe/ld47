using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartOnDie : MonoBehaviour
{
    public GameRestarter restarter;

    public GameObject explosionPrefab;
    
    private void Awake()
    {
        GetComponent<Damageable>().OnDie += RestartGame;
    }

    private void RestartGame()
    {
        if (explosionPrefab) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        if (restarter != null) restarter.RestartWholeGame();
    }
}
