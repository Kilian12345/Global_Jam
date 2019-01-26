using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyController : MonoBehaviour
{
    #region Variables
    float neonFuel = 10f;
    float deathTimer = 3f;

    public float maxSpeed;
    public float baseAcceleration;
    float acceleration;
    public float driftAmount;

    float boostMaxSpeed;
    float boostAcceleration;

    public float boostTime;

    float currentSpeed;
    public float boostMultiplier = 3;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private ParticleSystem particle;
    #endregion

    #region Monobehavior Callbacks

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        acceleration = baseAcceleration;
    }

    private void FixedUpdate()
    {

        neonFuel -= Time.deltaTime;

        if (neonFuel > 0)
        {
            Debug.Log(Mathf.Floor(neonFuel));
            Movement();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                StartCoroutine(Boost());
            }
        }
        else
        {
            StartCoroutine(Death());
        }
    }

    #endregion

    #region Functions
    private void Movement()
    {
        // Get input
        float horizontal = -Input.GetAxis("Horizontal");

        // Auto-move
        rb.AddForce(transform.up * acceleration);

        //Allows rotation and increased movement
        Vector2 speed = transform.right * (horizontal * acceleration);
        rb.AddForce(speed);

        #region Rotation
        // Create car rotation
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
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(sr);
    }

    IEnumerator Boost()
    {
        boostAcceleration = baseAcceleration * boostMultiplier;
        acceleration = boostAcceleration;

        yield return new WaitForSeconds(boostTime);
        acceleration = baseAcceleration;
        Debug.Log("Wallah je boost plus");
    }
    #endregion

}
