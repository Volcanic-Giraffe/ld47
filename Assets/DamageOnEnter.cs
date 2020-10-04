using UnityEngine;

public class DamageOnEnter : MonoBehaviour
{
    public float Damage = 5;
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<Damageable>();

        if (damageable != null)
        {
            damageable.Damage(gameObject, Damage);
        }
    }
}
