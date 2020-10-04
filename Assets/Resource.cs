using UnityEngine;

public class Resource : MonoBehaviour
{
    public int Amount = 10;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Hero")
        {
            GameState.GameState.GetInstance().Resources += Amount;
            Destroy(this.gameObject);
        }
    }
}
