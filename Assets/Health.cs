using UnityEngine;

public class Health : MonoBehaviour
{
    public int Amount = 5;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Hero")
        {
            collision.rigidbody.gameObject.GetComponent<Damageable>().Heal(Amount);
            Destroy(this.gameObject);
        }
    }
}
