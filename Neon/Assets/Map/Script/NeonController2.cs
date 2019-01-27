using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonController2 : MonoBehaviour
{

    #region Energy Values//Life Points
    [SerializeField]
    float energy = 20f;
    [SerializeField]
    float energyRechargeAmount = 10f;
    float rechargeCooldownTimer = 5f;
    #endregion

    #region Base Values//Movement
    [SerializeField]
    float speed = 50f;
    float baseSpeed = 50f;
    float currentSpeed;
    float maxSpeed = 200f;
    [SerializeField]
    float driftAmount = 200f;

    float distanceTravelled = 0f;

    float deathTimer = 2f;
    #endregion

    #region Boost Values//Speed Boost
    bool isBoosting;
    float boostSpeed;
    [SerializeField]
    float boostTimeLeft = 3f;
    [SerializeField]
    float boostSpeedMultiplier = 3f;
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
            Debug.Log(Mathf.Floor(energy));
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
            energy -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(Death());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("I have collided");

        if (collision.gameObject.name == "Refuel Station")
        {
            energy = energy + energyRechargeAmount;
            speed = baseSpeed;
            StartCoroutine(SlowDown());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("I have exited");
        speed = baseSpeed;
    }

    IEnumerator Boost()
    {
        isBoosting = true;
        boostSpeed = baseSpeed * boostSpeedMultiplier;
        speed = boostSpeed;

        yield return new WaitForSeconds(boostTimeLeft);
        speed = baseSpeed;
        Debug.Log("I Am No Longer Boosting");
    }

    IEnumerator SlowDown()
    {
        if (isBoosting == true) //set player speed back to normal
        {
            speed = baseSpeed;
        }

        slowedSpeed = baseSpeed * slowedMultiplier;
        speed = slowedSpeed;

        yield return new WaitForSeconds(rechargeCooldownTimer);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(sr);
    }


    #endregion
}
