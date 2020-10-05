using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Hero")
        {
            // TOdO EXIT
            Destroy(this.gameObject);
        }
    }
}
