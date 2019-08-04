using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Attackable, Togglable
{
    Rigidbody2D rb;
    public AudioClip deathSound;
    public AudioClip painSound;
    public PlayerController player;
    public GameObject projectilePrefab;
    public GameObject keyPrefab;
    public short STARTING_HEALTH = 3;
    public float MAX_SPEED = 2.0f;
    public float ACCELERATION = 20.0f;
    public float FORCE_MULTIPLIER = 15.0f;
    public float SHOOTING_PERIOD = 2.0f;
    public float MAX_SHOOTING_ANGLE = 40.0f;
    public bool CAN_MOVE = true;
    public bool DROP_KEYS = true;
    public float KEY_DROP_RATE = 0.2f;
    float shootingTimer;
    short health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = STARTING_HEALTH;
        shootingTimer = SHOOTING_PERIOD;
    }

    void Update()
    {
        if (shootingTimer <= 0)
        {
            Shoot();
            shootingTimer = SHOOTING_PERIOD;
        }
        else {
            shootingTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Rotate();
        if (CAN_MOVE)
        {
            Move();
        }
    }

    private void Rotate()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            float angleRad = Mathf.Atan2(playerPosition.y - transform.position.y, playerPosition.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad;
            angleDeg -= 90;
            transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }
    }

    private void Move()
    {
        Vector3 newPosition = Vector3.Normalize(player.transform.position - transform.position);
        rb.velocity = Vector3.MoveTowards(rb.velocity, newPosition * MAX_SPEED, ACCELERATION * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        Vector3 toPlayer = Vector3.Normalize((player.transform.position - transform.position));
        Vector3 projectileStartPosition = transform.position + toPlayer;
        GameObject projectileObject = (GameObject) Instantiate(projectilePrefab, projectileStartPosition, transform.rotation);
        ProjectileController projectile = projectileObject.GetComponent<ProjectileController>();
        projectile.target = GetProjectileTarget(projectile.MAX_SPEED);
    }

    private void Die()
    {
        if (DROP_KEYS && Random.value < KEY_DROP_RATE)
        {
            GameObject keyObject = (GameObject) Instantiate(keyPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        }
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(this.gameObject);
    }

    public void Toggle()
    {
        CAN_MOVE = !CAN_MOVE;
    }

    private Vector3 GetProjectileTarget(float speed)
    {
        Vector3 toCurrentPlayer = Vector3.Normalize(player.transform.position - transform.position);
        if (Random.value < 0.5)
        {
            float timeToPlayer = ((player.transform.position - transform.position).magnitude) / speed;
            Vector3 playerFuturePosition = (player.velocity * timeToPlayer) + player.transform.position;
            Vector3 toFuturePlayer = Vector3.Normalize(playerFuturePosition - transform.position);
            if (Vector3.Angle(toCurrentPlayer, toFuturePlayer) > MAX_SHOOTING_ANGLE)
            {
                Vector3 newVector = Vector3.RotateTowards(toCurrentPlayer, toFuturePlayer, MAX_SHOOTING_ANGLE * Mathf.Deg2Rad, 0.0f);
                return newVector;
            }
            else
            {
                return toFuturePlayer;
            }
        }
        else
        {
            return toCurrentPlayer;
        }
    }

    public override void onAttack(GameObject attacker)
    {
        health--;
        if (health <= 0)
        {
            Die();
        } else
        {
            AudioSource.PlayClipAtPoint(painSound, transform.position);
            Vector3 newPosition = Vector3.Normalize(transform.position - attacker.transform.position);
            rb.velocity += new Vector2(newPosition.x, newPosition.y) * FORCE_MULTIPLIER;
        }
    }
}
