using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFollower : MonoBehaviour
{
    public PlayerDetector playerDetector;

    public LaserGun laser;
    public Transform cannonPoint;

    public float moveSpeed;
    public float patrolDelay;
    public float wallRay;

    private float playerXZmargin = 1.5f; // окно по осям в коротом враг будет видеть игрока (на одной линии, типа)  
    
    private Rigidbody _rig;    

    private GameObject _player;
    
    private float _patrolTimer;
    private Vector3 _startPosition;

    private int _current;
    private Vector3[] _directions = {Vector3.forward, Vector3.right, Vector3.back, Vector3.left};

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _player = playerDetector.GetPlayer();
        
        _current = 0; // random?
        _patrolTimer = patrolDelay;

        _startPosition = transform.position;
    }

    void Update()
    {
        _patrolTimer -= Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (laser.Charging) return;
        if (IsTheSameLineWithPlayer() && playerDetector.CanSeePlayer(transform.position))
        {
            var lookPos = _player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = rotation;
            StartCoroutine(Fire());
        }
        else
        {
            ContinuePatrolling();
        }
    }

    private IEnumerator Fire()
    {
        yield return laser.WaitForChargeAndFire();
        ContinuePatrolling();
    }

    private bool IsTheSameLineWithPlayer()
    {
        if (_player == null) return false;

        var mPos = transform.position;
        var pPos = _player.transform.position;

        return Math.Abs(pPos.x - mPos.x) < playerXZmargin || Math.Abs(pPos.z - mPos.z) < playerXZmargin;
    }

    private void ContinuePatrolling()
    {
        if (_patrolTimer < 0 || WallIsClose())
        {
            _patrolTimer = patrolDelay;
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
