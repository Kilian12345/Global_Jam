using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauje_Lower : MonoBehaviour
{
    PlayerEnergyController lightEnergy;

    float energy;
    Vector3 baseScale;
    public Vector3 Scale;

    [SerializeField]
    float percentage;

    void Start()
    {
        lightEnergy = FindObjectOfType<PlayerEnergyController>();

        baseScale = transform.localScale;
        energy = lightEnergy.neonFuel;
        Scale = transform.localScale;
    }

    void Update()
    {
        Discharge();

    }

    void Discharge()
    {
    
          percentage = (lightEnergy.neonFuel * 100) / energy;

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
