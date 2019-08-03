using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10.0f;
    List<Interactable> nearbyInteractables = new List<Interactable>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        float translationY = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
        float translationX = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;

        transform.Translate(translationX, translationY, 0);
    }

    private void Interact() {
        for(int i = 0; i < nearbyInteractables.Count; i++)
        {
            nearbyInteractables[i].onInteract();
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Interactable other = otherCollider.gameObject.GetComponent<Interactable>();
        if (other != null) {
            nearbyInteractables.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        Interactable other = otherCollider.gameObject.GetComponent<Interactable>();
        if (other != null) {
            nearbyInteractables.Remove(other);
        }
    }
}
