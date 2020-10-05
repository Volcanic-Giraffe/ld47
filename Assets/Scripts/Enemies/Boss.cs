using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Damageable[] parts;
    private CoreMain _core;

    public GameObject keyPrefab;
    public Transform keySpawn;

    public GameObject dieExplosion;
    
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


        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        for (int i = 0; i < 9; i++)
        {
            var pos = transform.position;
            
            Vector3 random = new Vector3(pos.x + Random.Range(-2f,2f), pos.y, pos.x + Random.Range(-1f, 1f));
            
            Instantiate(dieExplosion, random, Quaternion.identity);
            
            yield return new WaitForSeconds(0.3f);            
        }

        Instantiate(keyPrefab, keySpawn.position, keySpawn.rotation);
        _core.OnBossDestroyed();
        
        Destroy(gameObject);
    }
}
