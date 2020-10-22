using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VaporBeam : MonoBehaviour
{
    private Transform _tank;

    private bool _ready;

    private HashSet<GameObject> _deathNote;
    
    // Start is called before the first frame update
    void Start()
    {
        _tank = GameObject.FindGameObjectWithTag("Hero").transform;
        _deathNote = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var lookPos = _tank.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
        
        _ready = true;
    }

    // OnTriggerEnter
    
    private void OnTriggerEnter(Collider other)
    {
        if (!_ready) return;
        
        if (other.CompareTag("Tile"))
        {
            var tile = other.gameObject.GetComponent<TileCell>();
            tile.OnVaporVisited();
        }


    }
    
    
}
