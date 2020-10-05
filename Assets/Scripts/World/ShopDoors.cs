using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDoors : MonoBehaviour
{
    public Transform doorL;
    public Transform doorR;
    public Transform arrow;
    

    private float _closedY;
    void Start()
    {
        _closedY = doorL.transform.position.y;

        arrow.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            OpenDoors();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            CloseDoors();
        }
    }

    public void OpenDoors()
    {
        arrow.gameObject.SetActive(true);
        
        var posL = doorL.transform.position;
        doorL.transform.position = new Vector3(posL.x, _closedY - 5, posL.z);
        
        var posR = doorR.transform.position;
        doorR.transform.position = new Vector3(posR.x, _closedY - 5, posR.z);
    }

    public void CloseDoors()
    {
        arrow.gameObject.SetActive(false);
        
        var posL = doorL.transform.position;
        doorL.transform.position = new Vector3(posL.x, _closedY, posL.z);
        
        var posR = doorR.transform.position;
        doorR.transform.position = new Vector3(posR.x, _closedY, posR.z);
    }

    public void LockEntrance()
    {
        
    }

    public void UnlockEntrance()
    {
        
    }
}
