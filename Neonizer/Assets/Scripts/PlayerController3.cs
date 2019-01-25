using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    #region Variables

    public float maxSpeed;
    public float acceleration;

    float currentSpeed;

    #endregion

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.AddForce(transform.up * (acceleration * Input.GetAxis("Vertical")));
        rb.AddForce(transform.right * (acceleration * Input.GetAxis("Horizontal")));

        // Force max speed limit
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        currentSpeed = rb.velocity.magnitude;

    }
}
