using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    public int Amount = 10;
    private GameObject _player;
    private Rigidbody _rb;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Hero");
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_player != null && Vector3.Distance(_player.transform.position, transform.position) < 4)
        {
            _rb.AddForce((_player.transform.position - transform.position) * 0.1f, ForceMode.VelocityChange);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Hero")
        {
            GameState.GameState.GetInstance().Resources += Amount;
            Destroy(this.gameObject);
        }
    }
}
