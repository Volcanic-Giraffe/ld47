using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum BotState {
    Idle,
    Agro,
    WaitForRetreat,
    Retreat
}

public class SimpleFollower : MonoBehaviour
{
    private GameObject _player;

    private Rigidbody _rig;

    private Animator _anim;
    private AudioSource _aud;
    public PlayerDetector playerDetector;
    
    public float moveSpeed;
    public float turnSpeed;
    public float aggroRadius = 5;

    public float retreatWait;
    
    private Vector3 _startPosition;
    
    public float DirectVisionRange = 8; // half a corridor

    private BotState _state;

    private float _retreatTimer;
    
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _aud = GetComponent<AudioSource>();
        _startPosition = transform.position;

        _state = BotState.Idle;
        
        _player = playerDetector.GetPlayer();
    }

    void Update()
    {
        if (_retreatTimer > 0) _retreatTimer -= Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        if (_state == BotState.Idle || _state == BotState.WaitForRetreat)
        {
            _aud.Stop();
            if (PlayerIsAngeringMe())
            {
                _state = BotState.Agro;
            }
        }

        if (_state == BotState.WaitForRetreat)
        {
            _anim.SetBool(IsWalking, false);
            
            if (_retreatTimer <= 0) _state = BotState.Retreat;
        }
        
        if (_state == BotState.Retreat)
        {
            LookAtPosition(_startPosition);
            var dist = MoveAtPosition(_startPosition);
            if (dist < 0.5) _state = BotState.Idle;
            if (!_aud.isPlaying) _aud.Play();
        }

        if (_state == BotState.Agro)
        {
            if (PlayerIsAngeringMe())
            {
                LookAtPlayer();
                MoveAtPlayer();
                if (!_aud.isPlaying) _aud.Play();
            }
            else
            {
                _state = BotState.WaitForRetreat;
                _retreatTimer = retreatWait;
            }
        }
    }

    private bool PlayerIsAngeringMe()
    {
        if (_player == null) return false;
        
        var distance = Vector3.Distance(_startPosition, _player.transform.position);
        if (distance < aggroRadius) return true;
        
        return playerDetector.CanSeePlayer(_startPosition, DirectVisionRange);
    }

    private void LookAtPlayer()
    {
        if (_player == null) return;
        LookAtPosition(_player.transform.position);
    }

    private void LookAtPosition(Vector3 position)
    {
        var localTarget = transform.InverseTransformPoint(position);
     
        var angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
 
        var eulerAngleVelocity = new Vector3 (0, angle, 0);
        var deltaRotation = Quaternion.Euler(eulerAngleVelocity * (Time.deltaTime * turnSpeed));
        _rig.MoveRotation(_rig.rotation * deltaRotation);
    }
    
    private float MoveAtPlayer()
    {
        if (_player == null) return -1f;
        return MoveAtPosition(_player.transform.position);
    }

    private float MoveAtPosition(Vector3 target)
    {
       
        var targetDir = target - transform.position;
        
        var distance = Vector3.Distance(transform.position, target);

        if (distance > 0.5f)
        {
            _rig.MovePosition(_rig.position + targetDir.normalized * (moveSpeed * Time.fixedDeltaTime));
            _anim.SetBool(IsWalking, true);
        }
        else
        {
            _anim.SetBool(IsWalking, false);
        }

        return distance;
    }
}
