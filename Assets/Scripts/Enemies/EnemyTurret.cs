using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public float turnSpeed;
    public float visionRadius;
    public float readyToAttackAngle;

    public Cannon cannon;
    public PlayerDetector playerDetector;
    
    private Rigidbody _rig;

    private GameObject _player;

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _player = playerDetector.GetPlayer();
    }


    private void FixedUpdate()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        if (_player == null) return;
        var distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance > visionRadius) return;
        
        var localTarget = transform.InverseTransformPoint(_player.transform.position);
     
        var angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        
        if (angle < readyToAttackAngle && playerDetector.CanSeePlayer(transform.position))
        {
            cannon.Fire();
        }
        
        var eulerAngleVelocity = new Vector3 (0, angle, 0);
        var deltaRotation = Quaternion.Euler(eulerAngleVelocity * (Time.deltaTime * turnSpeed));

        _rig.MoveRotation(_rig.rotation * deltaRotation);
    }
}
