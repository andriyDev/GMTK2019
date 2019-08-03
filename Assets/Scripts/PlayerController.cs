using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float translationY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float translationX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(translationX, translationY, 0);
    }
}
