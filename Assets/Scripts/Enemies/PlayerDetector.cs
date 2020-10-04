using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private GameObject _player;

    private float range = 50f;
    
    private void Start()
    {
        FindPlayer();
    }

    public GameObject GetPlayer()
    {
        FindPlayer();
        return _player;
    }

    public bool CanSeePlayer(Vector3 hostPos)
    {
        if (_player == null) return false;
        
        Vector3 targetPos = _player.transform.position;
        Ray ray = new Ray(hostPos, (targetPos - hostPos).normalized * range);
        RaycastHit hit;

        int mask = LayerMask.GetMask("Player", "Wall", "Default");

        if (Physics.Raycast(ray, out hit, range, mask))
        {
            if (hit.collider.gameObject.CompareTag("Hero"))
            {
                return true;
            }
        }
        return false;
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
