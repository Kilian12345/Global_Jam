using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border_Slow : MonoBehaviour
{
    float distance;
    float Basedistance;
    float distancePercentage;
    float slower;
    [SerializeField]
    ParticleSystem ps;

    public Vector3 Scale;
    Vector3 baseScale;

    void Start()
    {
        Scale = transform.localScale;
        baseScale = transform.localScale;

        ps = GetComponent<ParticleSystem>();
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


            var sz = ps.sizeOverLifetime;
            sz.enabled = true;

            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 0.1f);
            curve.AddKey(0.75f, 1.0f);

            sz.size = new ParticleSystem.MinMaxCurve(1.5f, curve);
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
        }
        if (distancePercentage > 100)
        {
            transform.localScale = baseScale;

        }



    }

}
/*    void Start() {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var sz = ps.sizeOverLifetime;
        sz.enabled = true;

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.1f);
        curve.AddKey(0.75f, 1.0f);

        sz.size = new ParticleSystem.MinMaxCurve(1.5f, curve);
    }*/
