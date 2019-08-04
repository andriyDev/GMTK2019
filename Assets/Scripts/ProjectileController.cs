using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector3 target;
    public float MAX_SPEED = 4.0f;
    public float ACCELERATION = 100.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (target != null)
        {
            rb.velocity = Vector3.MoveTowards(rb.velocity, target * MAX_SPEED, ACCELERATION * Time.fixedDeltaTime);
        }
    }
}
