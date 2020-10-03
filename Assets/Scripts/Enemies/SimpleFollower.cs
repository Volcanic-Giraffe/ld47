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
    
    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        
        FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        LookAtPlayer();

    }

    private void LookAtPlayer()
    {
        if (_player == null) return;
        
        var targetDir = _player.transform.position - transform.position;
        var forward = transform.forward;
        var localTarget = transform.InverseTransformPoint(_player.transform.position);
     
        var angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
 
        var eulerAngleVelocity = new Vector3 (0, angle, 0);
        var deltaRotation = Quaternion.Euler(eulerAngleVelocity * (Time.deltaTime * turnSpeed));
        _rig.MoveRotation(_rig.rotation * deltaRotation);
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
