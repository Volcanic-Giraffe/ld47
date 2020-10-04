using UnityEngine;
using System.Collections.Generic; 

[RequireComponent(typeof(Damageable))]
public class Mine : MonoBehaviour
{
    public List<string> TriggerTags = new List<string>();
    void OnCollisionEnter(Collision collision)
    {
        if(TriggerTags.Contains(collision.gameObject.tag)) {
            GetComponent<Damageable>().Damage(gameObject, 100);
        }
    }
}
