using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Damageable[] parts;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var alive = false;
        
        foreach (var part in parts)
        {
            if (part != null && part.IsAlive())
            {
                alive = true;
            }
        }
        
        if(!alive) Destroy(gameObject);
    }
}
