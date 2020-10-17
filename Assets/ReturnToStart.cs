using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToStart : MonoBehaviour
{
    public Vector3 ReturnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hero")
        {
            other.attachedRigidbody.transform.position = ReturnPoint;
        }
    }
}
