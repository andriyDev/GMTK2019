using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10.0f;
    List<Interactable> nearbyInteractables = new List<Interactable>();
    public AttackHandler attacker;
    public InteractHandler interacter;

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
        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float translationY = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
        float translationX = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;

        transform.Translate(translationX, translationY, 0);
    }

    private void Interact() {
        for(int i = 0; i < interacter.nearbyInteractables.Count; i++)
        {
            interacter.nearbyInteractables[i].onInteract();
        }
    }

    private void Attack() {
        for(int i = 0; i < attacker.nearbyAttackables.Count; i++)
        {
            attacker.nearbyAttackables[i].onAttack();
        }
    }
}
