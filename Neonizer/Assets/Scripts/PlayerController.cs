using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public float maxSpeed;
    public float baseAcceleration;
    public float acceleration;
    public float steering;

    float boostMaxSpeed;
    float boostAcceleration;

    public float boostTime;

    float currentSpeed;
    public float Juice = 3;


    #endregion

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        acceleration = baseAcceleration;
    }

    private void FixedUpdate()
    {
#region Base Movement
        // Get input
        float horizontal = -Input.GetAxis("Horizontal");
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
#endregion

        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine(Boost());
        }
    }

    IEnumerator Boost()
    {
        boostAcceleration = baseAcceleration * Juice;
        acceleration = boostAcceleration;
        yield return new WaitForSeconds(boostTime);
        acceleration = baseAcceleration;
        Debug.Log("put");


    }

    
}
