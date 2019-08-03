using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Attackable
{
    Rigidbody2D rb;
    public AudioClip deathSound;
    public AudioClip painSound;
    public PlayerController player;
    public short STARTING_HEALTH = 3;
    public float MAX_SPEED = 2.0f;
    public float ACCELERATION = 20.0f;
    public float FORCE_MULTIPLIER = 15.0f;
    short health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = STARTING_HEALTH;
        Shoot();
    }

    void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        Vector3 playerPosition = player.transform.position;
        float angleRad = Mathf.Atan2(playerPosition.y - transform.position.y, playerPosition.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        angleDeg -= 90;
        transform.rotation = Quaternion.Euler(0, 0, angleDeg);
    }

    private void Move()
    {
        Vector3 newPosition = Vector3.Normalize(player.transform.position - transform.position);
        rb.velocity = Vector3.MoveTowards(rb.velocity, newPosition * MAX_SPEED, ACCELERATION * Time.fixedDeltaTime);
    }

    private void Shoot()
    {

    }

    public override void onAttack() {
        health--;
        if (health <= 0) {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Destroy(this.gameObject);
        } else {
            AudioSource.PlayClipAtPoint(painSound, transform.position);
            Vector3 newPosition = Vector3.Normalize(transform.position - player.transform.position);
            rb.velocity += new Vector2(newPosition.x, newPosition.y) * FORCE_MULTIPLIER;
        }
    }
}
