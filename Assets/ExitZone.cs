using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Hero")
        {
            // TOdO EXIT
            Destroy(this.gameObject);
        }
    }
}
