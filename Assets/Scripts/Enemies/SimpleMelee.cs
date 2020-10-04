using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMelee : MonoBehaviour
{
    private GameObject _player;

    public PlayerDetector playerDetector;
    
    public GameObject punchProjectile;
    public Transform punchSpawn;
    
    public Animator _anim;
    public float attackRadius;

    private int DoAttack = Animator.StringToHash("DoAttack");

    public float attackRate;
    
    private float attackTimer; 
    
    // Start is called before the first frame update
    void Start()
    {
        _player = playerDetector.GetPlayer();
    }
    
    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }
    
    private void FixedUpdate()
    {
        if (attackTimer <= 0)
        {
            attackTimer = attackRate;
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (_player == null) return;
        var distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance > attackRadius) return;
        
        _anim.SetTrigger(DoAttack);
    }

    // triggered by animation events
    public void SpawnPunch()
    {
        Instantiate(punchProjectile, punchSpawn.position, transform.rotation);
    }
}
