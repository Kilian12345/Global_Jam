using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauje_Lower : MonoBehaviour
{
    PlayerEnergyController lightEnergy;

    float energy;
    Vector3 baseScaleY;
    Vector3 ScaleY;

    void Start()
    {
        transform.localScale = baseScaleY;
        ScaleY = transform.localScale;
        energy = lightEnergy.neonFuel;

    }




    void Discharge()
    {
        ScaleY.y = 2f;
    }

}
