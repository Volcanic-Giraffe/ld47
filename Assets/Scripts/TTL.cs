using UnityEngine;

public class TTL : MonoBehaviour
{
    public float DieAfter = 3;
    void Update()
    {
        DieAfter -= Time.deltaTime;
        if (DieAfter <= 0) Destroy(this.gameObject);
    }
}
