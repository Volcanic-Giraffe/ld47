using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRBController : MonoBehaviour
{
    public PpVolumeController ppVolume;
    
    public float Speed = 5f;
    public LayerMask Ground;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;

    private Damageable _damageable;
    
    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _damageable.OnDie += OnTankDie;
        _damageable.OnHit += OnTankHit;
    }

    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;
    }


    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }

    void OnTankHit()
    {
        Debug.Log("### TANK HIT!");
        ppVolume.AnimateOnHit();
    }

    void OnTankDie()
    {
        
    }
}
