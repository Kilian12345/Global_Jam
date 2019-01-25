using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Objectif : MonoBehaviour
{
    [SerializeField]
    Transform limite1;
    [SerializeField]
    Transform limite2;
    [SerializeField]
    GameObject oldObjectif;

    public GameObject objectif;
    public bool IsStill = false;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Respawn();
            Spawn();
        }
    }


    void Spawn()
    {
        if (IsStill == false)
        {
            Vector2 position = new Vector2(0, Random.Range(limite1.position.y, limite2.position.y));
            position.y = Mathf.Floor(position.y);
            oldObjectif = Instantiate(objectif, position, Quaternion.identity);
            IsStill = true;

        }
    }

    void Respawn()
    {
        if (IsStill == true)
        {
            Destroy(oldObjectif);
            IsStill = false;
            Debug.Log("Jte soule");
        }

    }

}
