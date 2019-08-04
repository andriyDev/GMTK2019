using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Attackable
{
    public AudioClip painSound;
    public AudioClip loseSound;
    public float MAX_SPEED = 9.0f;
    public float ACCELERATION = 100.0f;
    public float SWORD_ANIMATION_TIME = 0.5f;
    public int SWORD_MAX_ROTATION = 65;
    public float FORCE_MULTIPLIER = 15.0f;
    public short STARTING_HEALTH = 3;
    public AttackHandler attacker;
    public InteractHandler interacter;
    public GameObject swordRoot;
    Rigidbody2D rb;
    List<Interactable> nearbyInteractables = new List<Interactable>();
    float swordAnimationTimer = -1;
    short health;

    public Vector3 velocity
    {
        get {
            return rb.velocity;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = STARTING_HEALTH;
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
        if (Input.GetButtonDown("Attack"))
        {
            if (!IsAttacking()) {
                Attack();
            }
        }
        AnimateSword();
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

    private void Interact()
    {
        for(int i = 0; i < interacter.nearbyInteractables.Count; i++)
        {
            interacter.nearbyInteractables[i].onInteract();
        }
    }

    private void Attack()
    {
        swordAnimationTimer = SWORD_ANIMATION_TIME;
        for(int i = 0; i < attacker.nearbyAttackables.Count; i++)
        {
            attacker.nearbyAttackables[i].onAttack(this.gameObject);
        }
    }

    private bool IsAttacking()
    {
        return swordAnimationTimer > 0;
    }

    private void AnimateSword()
    {
        if (IsAttacking()) {
            float normalizedAnimationTime = (SWORD_ANIMATION_TIME - swordAnimationTimer) / SWORD_ANIMATION_TIME;
            swordRoot.transform.localRotation = Quaternion.Euler(0, 0, -normalizedAnimationTime * SWORD_MAX_ROTATION);
            swordAnimationTimer -= Time.deltaTime;
        } else {
            swordRoot.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public override void onAttack(GameObject attacker)
    {
        health--;
        if (health <= 0) {
            AudioSource.PlayClipAtPoint(loseSound, transform.position);
            Destroy(this.gameObject);
        } else {
            AudioSource.PlayClipAtPoint(painSound, transform.position);
            Vector3 newPosition = Vector3.Normalize(transform.position - attacker.transform.position);
            rb.velocity += new Vector2(newPosition.x, newPosition.y) * FORCE_MULTIPLIER;
        }
    }
}
