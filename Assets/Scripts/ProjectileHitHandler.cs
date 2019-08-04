using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        PlayerController other = otherCollider.gameObject.GetComponent<PlayerController>();
        GameObject parentObject = transform.parent.gameObject;
        if (other != null)
        {
            other.onAttack(parentObject);
        }
        if (!otherCollider.isTrigger)
        {
            Destroy(parentObject);
        }
    }
}
