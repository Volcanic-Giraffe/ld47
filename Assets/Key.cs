using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private GameObject hero;
    private Rigidbody _rb;
    public AudioClip pickupSound;
    // Start is called before the first frame update

    private bool _pickOnce;
    
    private void Start()
    {
        var hs = GameObject.FindGameObjectsWithTag("Hero");
        foreach (var he in hs)
        {
            if (he.GetComponent<TankUpgrades>() != null)
            {
                hero = he;
                break;
            }
        }
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_pickOnce) return;
        
        if (hero != null && Vector3.Distance(hero.transform.position, transform.position) < 4)
        {
            _rb.AddForce((hero.transform.position - transform.position).normalized * 0.5f, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Hero")
        {
            if (_pickOnce) return;
            _pickOnce = true;
            
            if (pickupSound != null) AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);

            hero.GetComponent<TankUpgrades>().AddKey();
            
            Destroy(gameObject);
        }
    }
}
