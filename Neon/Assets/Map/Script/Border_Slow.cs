using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border_Slow : MonoBehaviour
{
    float distance;
    float Basedistance;
    float distancePercentage;
    float slower;

    public GameObject ps;
    Vector3 kouillas;
    Vector3 psSize;

    public Vector3 Scale;
    Vector3 baseScale;


    void Start()
    {
        Scale = transform.localScale;
        baseScale = transform.localScale;
        kouillas = ps.transform.localScale;
    }

    void Update()
    {
        Rescale();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            distance = Vector2.Distance((new Vector2(collision.transform.position.x, collision.transform.position.y)), (new Vector2(transform.position.x, transform.position.y)));
            Basedistance = distance;
            Debug.Log("REGARDE"+ Basedistance);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            distance = Vector2.Distance((new Vector2(collision.transform.position.x, collision.transform.position.y)), (new Vector2(transform.position.x, transform.position.y)));
            distancePercentage = (distance * 100) / Basedistance;

            Debug.Log(distancePercentage);
        }

    }

    void Rescale()
    {
        if (distancePercentage <= 100)
        {

            Scale = (distancePercentage * baseScale) / 100;
            transform.localScale = Scale;

           psSize = (distancePercentage * kouillas) / 100;
            ps.transform.localScale = psSize;

            Debug.Log(psSize);
        }
        if (distancePercentage > 100)
        {
            transform.localScale = baseScale;
            ps.transform.localScale = kouillas;

        }



    }

}

