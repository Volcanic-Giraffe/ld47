using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Exploding : MonoBehaviour
{

    public GameObject Explosion;

    void Start()
    {
        GetComponent<Damageable>().OnDie += Exploding_OnDie;
    }

    private void Exploding_OnDie()
    {
        Instantiate(Explosion, this.transform.position, Quaternion.identity);
    }

}
