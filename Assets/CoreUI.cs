using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreUI : MonoBehaviour
{
    public GameObject Health;
    int health = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var actualHealth = (int)Mathf.Round(GameState.GameState.GetInstance().HeroHealth / 5);
        if (health != actualHealth)
        {
            health = actualHealth;
            for (int i = 0; i < Health.transform.childCount; i++)
            {
                Health.transform.GetChild(i).gameObject.SetActive(i < actualHealth);
            }

        }
    }
}
