using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollower : MonoBehaviour
{
    private GameObject _player;

    private Rigidbody _rig;
    
    public float moveSpeed;
    public float turnSpeed;
    public float visionRadius;

    private Vector3 _startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _startPosition = transform.position;

        FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
        MoveAtPlayer();
    }

    private void LookAtPlayer()
    {
        if (_player == null) return;
        var distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance > visionRadius) return;
        
        var targetDir = _player.transform.position - transform.position;
        var forward = transform.forward;
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

        Debug.Log(distanceFromStart);
        
        var target = _player.transform.position;
        if (distanceFromStart > visionRadius)
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

    private void FindPlayer()
    {
        if (_player == null)
        {
            var objs = GameObject.FindGameObjectsWithTag("Hero");

            if (objs != null && objs.Length > 0)
            {
                _player = objs[0];
            }
        }
    }
}
