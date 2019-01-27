﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonController2 : MonoBehaviour
{

    #region Energy Values//Life Points
    [SerializeField]
    float energy;
    [SerializeField]
    float energyRechargeAmount;
    float rechargeCooldownTimer = 5f;
    #endregion

    #region Base Values//Movement
    [SerializeField]
    float speed = 50;
    float baseSpeed = 50;
    float currentSpeed;
    float maxSpeed = 200;
    [SerializeField]
    float driftAmount;

    float distanceTravelled = 0;

    float distance;
    float Basedistance;
    float distancePercentage;

    float deathTimer = 2f;
    #endregion

    #region Boost Values//Speed Boost
    bool isBoosting;
    float boostSpeed;
    [SerializeField]
    float boostTimeLeft;
    [SerializeField]
    float boostSpeedMultiplier;
    #endregion

    #region Slow Down Values//LightBoxes
    float slowedSpeed;
    float slowedMaxSpeed;
    [SerializeField]
    float slowedMultiplier = 0.3f;
    #endregion

    #region Intantiates//Objects, etc.
    Vector2 lastPosition;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private ParticleSystem particle;
    private Collider2D collider;
    #endregion

    #region MonoBehavior Callbacks
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = baseSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (energy > 0)
        {
           // Debug.Log(Mathf.Floor(energy));
            Move();
        }

        else
        {
            //nothing
        }
    }
    #endregion

    #region Functions and Coroutines
    private void Move()
    {
        #region Movement
        float horizontal = -Input.GetAxis("P2_Horizontal");

        rb.AddForce(transform.up * speed); //auto-movement

        rb.AddForce(transform.right * (horizontal * speed)); //rotation based on player input
        #endregion
        // Get inputs for basic movement

        #region Rotation
        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));

        if (direction >= 0.0f)
        {
            rb.rotation += horizontal * driftAmount * (rb.velocity.magnitude / maxSpeed);
        }

        else
        {
            rb.rotation -= horizontal * driftAmount * (rb.velocity.magnitude / maxSpeed);
        }
        #endregion
        // Create Neon rotation

        #region Drifting
        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left));

        Vector2 relativeForce = Vector2.right * driftForce * 0.00005f;

        Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);

        rb.AddForce(rb.GetRelativeVector(relativeForce));
        #endregion
        // Change velocity based on rotation

        float currentSpeedX = rb.velocity.magnitude;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (speed == baseSpeed)
            {
                StartCoroutine(Boost());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        speed = baseSpeed;
    }

    IEnumerator Boost()
    {
        isBoosting = true;
        boostSpeed = baseSpeed * boostSpeedMultiplier;
        speed = boostSpeed;

        yield return new WaitForSeconds(boostTimeLeft);
        speed = baseSpeed;

    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(sr);
    }


    #endregion
}
