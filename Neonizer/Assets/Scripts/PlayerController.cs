using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public float maxSpeed;
    public float acceleration;
    public float steering;

    public float boostMaxSpeed;
    public float boostAcceleration;

    public float boostTime;

    float currentSpeed;

    #endregion

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Get input
        float horizontal = -Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Auto-move
        rb.AddForce(transform.up * acceleration);

        //Allows rotation increased movement
        Vector2 speed = transform.up * (vertical * acceleration);
        rb.AddForce(speed);

        // Create car rotation
        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));

        if (direction >= 0.0f)
        {
            rb.rotation += horizontal * steering * (rb.velocity.magnitude / maxSpeed);
        }

        else
        {
            rb.rotation -= horizontal * steering * (rb.velocity.magnitude / maxSpeed);
        }

        // Change velocity based on rotation
        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;

        Vector2 relativeForce = Vector2.right * driftForce;

        Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);

        rb.AddForce(rb.GetRelativeVector(relativeForce));

        // Force max speed limit
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        currentSpeed = rb.velocity.magnitude;


        if (Input.GetButtonDown("Shift"))
        {
            StartCoroutine(Boost());
        }
    }

    IEnumerator Boost()
    {
        maxSpeed = boostMaxSpeed;
        acceleration = boostAcceleration;

        yield return new WaitForSeconds(boostTime);
    }

}
