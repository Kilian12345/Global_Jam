using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    public float driftStart;
    public float maxSpeed;
    public float moveForce;
    public float rotationSpeed;

    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move car "forward"
        rigidbody2D.AddForce(transform.right * moveForce * Input.GetAxis("Vertical"));

        //limit car velocity    
        rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maxSpeed);

        //turning the car 
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            //rotate body 
            rigidbody2D.angularVelocity = Input.GetAxis("Horizontal") * -rotationSpeed;
            
            //increase rotation force 
            if (rotationSpeed < 300)
            {
                rotationSpeed += Time.deltaTime * 20;
            }

            //add opposite force (drift)
            rigidbody2D.AddForce(-transform.up * (rigidbody2D.velocity.magnitude / 2) * driftStart * Input.GetAxis("Horizontal"));
            
            //decrease drifting multiplier 
            if (driftStart > 0.2f)
            {
                driftStart -= 0.01f;
            }
            
            //decrease general speed 
            if (maxSpeed < 10)
            {
                maxSpeed -= 0.5f;
            }
        }

        /*else
        {
            //reset 
            driftStart = startDriftStart;
            rotationSpeed = startRotation;
            maxSpeed = startMxSpeed;

        }*/

    }
}
