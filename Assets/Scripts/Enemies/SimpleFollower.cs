using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollower : MonoBehaviour
{
    private GameObject _player;

    private Rigidbody _rig;
    
    public PlayerDetector playerDetector;
    
    public float moveSpeed;
    public float turnSpeed;
    public float aggroRadius = 5;
    public float agroDelay;
    
    private Vector3 _startPosition;
    
    private float agroTimer;
    public float DirectVisionRange = 8; // half a corridor


    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _startPosition = transform.position;

        agroTimer = 0;
        
        _player = playerDetector.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (agroTimer > 0) agroTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // закомментируй это, если нужно чтобы бот нападал только в радиусе своего вижена (старое поведение)
        if (agroTimer <= 0 && playerDetector.CanSeePlayer(_startPosition, DirectVisionRange))
        {
            agroTimer = agroDelay;
        }
        
        LookAtPlayer();
        MoveAtPlayer();
    }

    private void LookAtPlayer()
    {
        if (_player == null) return;
        var distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance > aggroRadius && agroTimer <= 0) return;
        
        var localTarget = transform.InverseTransformPoint(_player.transform.position);
     
        var angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
 
        var eulerAngleVelocity = new Vector3 (0, angle, 0);
        var deltaRotation = Quaternion.Euler(eulerAngleVelocity * (Time.deltaTime * turnSpeed));
        _rig.MoveRotation(_rig.rotation * deltaRotation);
    }

    private void MoveAtPlayer()
    {
        if (_player == null) return;
        var distanceFromStart = Vector3.Distance(_startPosition, _player.transform.position);

        var target = _player.transform.position;
        if (distanceFromStart > aggroRadius && agroTimer <= 0)
        {
            target = _startPosition;
        }
        
        var targetDir = target - transform.position;
        
        var distance = Vector3.Distance(transform.position, target);

        if (distance > 0.5f)
        {
            _rig.MovePosition(_rig.position + targetDir.normalized * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}
