using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : NeonController
{
    public void MoveP1()
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

        float currentSpeedX = rb.velocity.magnitude;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (speed == baseSpeed)
            {
                StartCoroutine(Boost());
            }
        }

    }
}
