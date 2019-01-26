using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonController : MonoBehaviour
{
    #region Variables

    #region Energy Values
    [SerializeField]
    float neonEnergy;
    [SerializeField]
    float energyRechargeAmount;
    #endregion

    #region Base Values
    [SerializeField]
    float speed;
    [SerializeField]
    float currentSpeed;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float baseSpeed;
    [SerializeField]
    float driftAmount;
    float distanceTravelled = 0;
    #endregion

    #region Intantiates
    Vector2 lastPosition;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private ParticleSystem particle;
    private Collider2D collider;
    #endregion

    #endregion

    #region MonoBehavior Callbacks
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = baseSpeed;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(neonEnergy > 0)
        {
            Debug.Log(Mathf.Floor(neonEnergy));
            Move();
        }

        Mathf.Floor(distanceTravelled += Vector2.Distance(transform.position, lastPosition));
        lastPosition = transform.position;

        Debug.Log(distanceTravelled);
    }
    #endregion

    #region Functions and Coroutines
    private void Move()
    {
        #region Movement
        float horizontal = -Input.GetAxis("Horizontal");

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
        // Create car rotation

        #region Drifting
        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left));

        Vector2 relativeForce = Vector2.right * driftForce * 0.00005f;

        Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);

        rb.AddForce(rb.GetRelativeVector(relativeForce));
        #endregion
        // Change velocity based on rotation
    }
    #endregion
}
