using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonController : MonoBehaviour
{

    #region Energy Values//Life Points
    public float energy = 50;
    [SerializeField]
    float energyRechargeAmount;
    float rechargeCooldownTimer= 5f; 
    #endregion

    #region Base Values//Movement
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float baseSpeed = 5;
    float currentSpeed;
    [SerializeField]
    float maxSpeed = 10;
    [SerializeField]
    float driftAmount = 200;

    float distanceTravelled = 0f;

    float deathTimer = 2f;
    #endregion

    #region Boost Values//Speed Boost
    bool isBoosting;
    [SerializeField]
    float boostSpeed = 50;
    [SerializeField]
    float boostTimeLeft = 2;
    [SerializeField]
    float boostSpeedMultiplier = 3;
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

    public Transform positionOrigin;
    Vector3 spawnPosition;
    public GameObject playerSecondLife;
    #endregion

    #region MonoBehavior Callbacks

    private void Awake()
    {
        spawnPosition = this.gameObject.transform.position;
        playerSecondLife = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = baseSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(energy > 0)
        {
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
        float horizontal = -Input.GetAxis("P1_Horizontal");

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

        Vector2 relativeForce = Vector2.right * driftForce * 0.05f;

        Debug.DrawLine( rb.position, rb.GetRelativePoint(relativeForce), Color.green);
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
            //StartCoroutine(Death());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.name == "Refuel Station")
        {
            energy = energy + energyRechargeAmount;
            speed = baseSpeed;
            StartCoroutine(SlowDown());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        speed = baseSpeed;
    }

    private void Respawn()
    {
        Instantiate(playerSecondLife, spawnPosition, Quaternion.identity);
    }

    IEnumerator Boost()
    {
        isBoosting = true;
        boostSpeed = baseSpeed * boostSpeedMultiplier;
        speed = boostSpeed;

        yield return new WaitForSeconds(boostTimeLeft);
        speed = baseSpeed;

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
        Respawn();
    }


    #endregion
}