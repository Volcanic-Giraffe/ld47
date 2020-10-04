using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFollower : MonoBehaviour
{
    public PlayerDetector playerDetector;

    public Transform cannonPoint;
    
    public float moveSpeed;
    public float turnSpeed;
    public float visionRadius;
    public float patrolDelay;
    public float wallRay;
    
    private Rigidbody _rig;    

    private GameObject _player;
    
    private float patrolTimer;
    private Vector3 _startPosition;

    private int _current;
    private Vector3[] _directions = {Vector3.forward, Vector3.right, Vector3.back, Vector3.left};
    
    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _current = 0; // random?
        patrolTimer = patrolDelay;

        _startPosition = transform.position;
    }

    void Update()
    {
        patrolTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (patrolTimer < 0 || WallIsClose())
        {
            patrolTimer = patrolDelay;
            _current = (_current + 1) % _directions.Length;
        }
        transform.forward = _directions[_current];
        _rig.MovePosition(_rig.position + _directions[_current] * (moveSpeed * Time.fixedDeltaTime));
    }

    private bool WallIsClose()
    {
        Ray ray = new Ray(cannonPoint.position, (cannonPoint.forward).normalized * wallRay);
        RaycastHit hit;

        int mask = LayerMask.GetMask("Wall", "Default");

        if (Physics.Raycast(ray, out hit, wallRay, mask))
        {
            return true;
        }
        return false;
    }
}
