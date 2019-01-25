using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Brain : MonoBehaviour
 {



    public float startWaitTime;
    public bool isChase = false;
    public bool isMoving = true;


    [SerializeField]
    Transform AI;
    [SerializeField]
    Transform Joueur;
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    Transform[] moveSpots;

    //Private
    private int Waypoints = 0;
    private float waitTime;
    float rotationSpeed = 3;
    Vector2 lastDirection;
    Vector3 lookAt;
    Transform GetClosestWaypoints;
    int index;
    public bool afterChasing = false;

    Transform closerMoveSpot;

    // Use this for initialization
    void Start ()
    {

        //Waypoints = Random.Range(0, moveSpots.Length);
        transform.position = moveSpots[Waypoints].transform.position;

        //Rotation AI
        lookAt = (Vector2)(moveSpots[Waypoints].position - AI.position);

    }
	
	// Update is called once per frame
	void Update ()
    {
        //Rotation
         if (isMoving)
            {
                transform.up = lookAt;
                lastDirection = transform.up;
            }

            else
            {
             transform.up = lastDirection;
            }

         //Patrol
        if (isChase == false)

        {
            if (afterChasing)
            {
                /*Vector3 followingPos = GetCloserMoveSpot().position;
                transform.position = Vector2.MoveTowards(transform.position, followingPos, speed * Time.deltaTime);
                if(transform.position == followingPos)
                {
                    afterChasing = false;

                }*/
                Waypoints = GetCloserMoveSpot();
                afterChasing = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, moveSpots[Waypoints].transform.position, speed * Time.deltaTime);
            }


                /*if (Vector2.Distance(transform.position, moveSpots[Waypoints].transform.position) < 0.2f)
                {
                    if (waitTime <= 0)
                    {
               //     Waypoints = Random.Range(0, moveSpots.Length);
                        waitTime = startWaitTime;
                    isMoving = true;
                }

                    else
                    {
                        waitTime -= Time.deltaTime;
                    isMoving = false;
                    }

                }  */        
        }
          

        //Chase
        if (isChase == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Joueur.position , speed * Time.deltaTime);
            Debug.Log("je cours");

            lookAt = (Vector2)(Joueur.position - transform.position).normalized;
            isMoving = true;
            afterChasing = true;

        }
        else
        {

           // FindClosestWay();
            lookAt = (Vector2)(moveSpots[Waypoints].position - AI.position);
            isMoving = true;
        }

        Debug.DrawLine(moveSpots[Waypoints].transform.position, transform.position, Color.red);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PatrolPoint")
        {
            Waypoints += 1;
        }
        if (Waypoints == moveSpots.Length)
        {
            Waypoints = 0;
            Debug.Log(moveSpots[Waypoints]);
        }
    }

    int GetCloserMoveSpot() {
        float closerDistance = Mathf.Infinity;
        foreach (Transform movespot in moveSpots)
        {
            float distance = (movespot.position - transform.position).sqrMagnitude;

            if (distance < closerDistance){
                closerDistance = distance;
                closerMoveSpot = movespot;
            }
        }
        for (int i = 0; i < moveSpots.Length - 1; i++)
        {
            if (moveSpots[i] == closerMoveSpot)
            {
                index = i;
            }
        }
        Debug.Log(closerMoveSpot);
        return (index);
    }

   /* void FindClosestWay()
    {
 
        float distanceToClosestWay = Mathf.Infinity;
        WayPoints_identity closestWay = null;
        WayPoints_identity[] allEnemies = GameObject.FindObjectsOfType<WayPoints_identity>();

        foreach (WayPoints_identity currentWay in allEnemies)
        {
            float distanceToWay = (currentWay.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToWay < distanceToClosestWay)
            {
                distanceToClosestWay = distanceToWay;
                closestWay = currentWay;
            }
        }


    }*/
}



