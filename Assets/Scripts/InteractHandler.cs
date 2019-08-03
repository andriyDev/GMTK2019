using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{
    private List<Interactable> _nearbyInteractables = new List<Interactable>();

    public List<Interactable> nearbyInteractables
    {
        get {
            return _nearbyInteractables;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Interactable other = otherCollider.gameObject.GetComponent<Interactable>();
        if (other != null) {
            _nearbyInteractables.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        Interactable other = otherCollider.gameObject.GetComponent<Interactable>();
        if (other != null) {
            _nearbyInteractables.Remove(other);
        }
    }
}
