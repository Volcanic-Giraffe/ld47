using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSimple : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private Vector3 _movement;

    private Rigidbody _rigidbody;

    public float Speed = 20;

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector3(
            Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce((_movement * (Speed * Time.fixedDeltaTime)), ForceMode.Impulse);
    }
}