using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallKeyed : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Hero")
        {
            // only attached has tag
            if (GameObject.FindGameObjectWithTag("Key") != null)
            {
                // TOdO sounds, fireworks, etc
                Destroy(this.gameObject);
            }
        }
    }
}
