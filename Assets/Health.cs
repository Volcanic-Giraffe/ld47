using UnityEngine;

public class Health : MonoBehaviour
{
    public int Amount = 5;
    public AudioClip pickupSound;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Hero")
        {
            if (pickupSound != null) AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);
            collision.rigidbody.gameObject.GetComponent<Damageable>().Heal(Amount);
            Destroy(this.gameObject);
        }
    }
}
