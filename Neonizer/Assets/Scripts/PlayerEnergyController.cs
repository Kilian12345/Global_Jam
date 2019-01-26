using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyController : MonoBehaviour
{
    #region Variables
    //FUEL VALUES
    [SerializeField]
    float neonFuel;
    [SerializeField]
    float fuelRecharge;
    [SerializeField]
    float deathTimer;

    //BASE VALUES
    float acceleration;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float baseAcceleration;
    [SerializeField]
    float driftAmount;

    //BOOST VALUES
    float boostMaxSpeed;
    float boostAcceleration;
    [SerializeField]
    float boostTime;

    //SLOW VALUES
    float slowedAcceleration;
    float slowedMaxSpeed;

    //SPEED MULTIPLIERS
    [SerializeField]
    float currentSpeed;
    [SerializeField]
    float boostMultiplier;
    [SerializeField]
    float slowedModifier;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private ParticleSystem particle;
    private Collider2D collider;
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
        if (neonFuel > 0)
        {
            Debug.Log(Mathf.Floor(neonFuel));
            Movement();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                StartCoroutine(Boost());
            }
            neonFuel -= Time.deltaTime;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("I have collided");
        neonFuel += fuelRecharge;
        StartCoroutine(SlowDown());
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

    IEnumerator SlowDown()
    {
        slowedAcceleration = baseAcceleration * slowedModifier;
        acceleration = slowedAcceleration;
        
        yield return new WaitForSeconds (2);
    }

    #endregion
}
