using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauje_Lower : MonoBehaviour
{
    NeonController lightEnergy;

    float Energy;
    Vector3 baseScale;
    public Vector3 Scale;

    [SerializeField]
    float percentage;

    void Start()
    {
        lightEnergy = FindObjectOfType<NeonController>();

        baseScale = transform.localScale;
        Energy = lightEnergy.energy;
        Scale = transform.localScale;
    }

    void Update()
    {
        Discharge();
        Debug.Log(lightEnergy.energy);

    }

    void Discharge()
    {
    
          percentage = (lightEnergy.energy * 100) / Energy;

            if (percentage <= 100)
            {

            Scale.y = (percentage * baseScale.y) / 100;
            transform.localScale = Scale;
            Debug.Log(Scale.y);

            }

            if (percentage > 100)
            {
            transform.localScale = baseScale;

            }
    }


}
