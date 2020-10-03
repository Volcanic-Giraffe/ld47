using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float Health = 20;

    public void Damage(GameObject who, float amount)
    {
        Debug.Log($"Damaging {gameObject.name} with {who.name} by {amount}.");
        Health -= amount;
        if (Health <= 0) Destroy(gameObject);
    }
}
