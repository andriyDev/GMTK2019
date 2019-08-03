using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Attackable
{
    Rigidbody2D rb;
    public PlayerController player;
    public float MAX_SPEED = 2.0f;
    public float ACCELERATION = 100.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    public override void onAttack() {
        Destroy(this.gameObject);
    }
}
