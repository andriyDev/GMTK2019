using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10.0f;
    public float MAX_SPEED = 9.0f;
    public float ACCELERATION = 100.0f;
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
        Rotate();
    }

    private void Move()
    {
        float translationY = Input.GetAxis("Vertical");
        float translationX = Input.GetAxis("Horizontal");
        rb.velocity = Vector3.MoveTowards(rb.velocity, new Vector3(translationX, translationY, 0) * MAX_SPEED, ACCELERATION * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, -5));
        float angleRad = Mathf.Atan2(mouseWorld.y - transform.position.y, mouseWorld.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        angleDeg -= 90;
        transform.rotation = Quaternion.Euler(0, 0, angleDeg);
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
