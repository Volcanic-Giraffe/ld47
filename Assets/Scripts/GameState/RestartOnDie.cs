using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartOnDie : MonoBehaviour
{
    public GameRestarter restarter;
    private void Awake()
    {
        GetComponent<Damageable>().OnDie += RestartGame;
    }

    private void RestartGame()
    {
        if (restarter != null) restarter.RestartWholeGame();
    }
}
