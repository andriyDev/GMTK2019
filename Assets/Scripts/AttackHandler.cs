using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    List<Attackable> _nearbyAttackables = new List<Attackable>();

    public List<Attackable> nearbyAttackables
    {
        get {
            return _nearbyAttackables;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Attackable other = otherCollider.gameObject.GetComponent<Attackable>();
        if (other != null) {
            _nearbyAttackables.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        Attackable other = otherCollider.gameObject.GetComponent<Attackable>();
        if (other != null) {
            _nearbyAttackables.Remove(other);
        }
    }
}
