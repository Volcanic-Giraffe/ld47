using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Damageable[] parts;
    private CoreMain _core;

    public GameObject keyPrefab;
    public Transform keySpawn;
    
    private bool _diedOnce;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetCore(CoreMain core)
    {
        _core = core;
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

        if (!alive)
        {
            DieBossDie();
        }
    }

    void DieBossDie()
    {
        if (_diedOnce) return;
        _diedOnce = true;
        
        // todo: add explosions
        // todo: delay destroy and core popup

        Instantiate(keyPrefab, keySpawn.position, keySpawn.rotation);
        
        _core.OnBossDestroyed();
        
        Destroy(gameObject);
        
    }
}
