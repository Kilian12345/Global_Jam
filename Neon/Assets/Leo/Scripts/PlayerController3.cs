using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
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

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = -Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        //Neon Movement
        rb.AddForce(transform.up * (acceleration * vertical));
        rb.AddForce(transform.right * (acceleration * horizontal));

        //Neon facing direction
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

        IEnumerator Boost()
        {
            maxSpeed = boostMaxSpeed;
            acceleration = boostAcceleration;

            yield return new WaitForSeconds(boostTime);
        }

    }
}
