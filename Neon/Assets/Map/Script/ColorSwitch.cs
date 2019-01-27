using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ColorSwitch : MonoBehaviour
{
    //Transform thisCirclePosition;
    public Transform positionOrigin;
    string thisPoint;

    Vector3 thisCirclePosition;
    public GameObject blueLightPoint;
    public GameObject redLightPoint;
    GameObject bluePoint;
    GameObject redPoint;
    GameObject whitePoint;
    NeonController2 playerScript;

    public Text scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        thisPoint = this.gameObject.tag;
        bluePoint = GameObject.FindGameObjectWithTag("PointBlue");
        redPoint = GameObject.FindGameObjectWithTag("PointRed");
        whitePoint = GameObject.FindGameObjectWithTag("PointWhite");
    }

    // Update is called once per frame
    void Update()
    {
        thisCirclePosition = this.gameObject.transform.position;
        thisPoint = this.gameObject.tag;
        Debug.Log("I am " + thisPoint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("I am Colliding with : " + other.gameObject.name);

        if (other.gameObject.tag == "Player_Red" && thisPoint != "PointRed")
        {
            Destroy(this.gameObject);
            Debug.Log("Je passe au Rouge");
            thisPoint = "PointRed";
            Instantiate(redLightPoint, thisCirclePosition, Quaternion.identity);
        }

        if (other.gameObject.tag == "Player_Blue" && thisPoint != "PointBlue")
        {
            Destroy(this.gameObject);
            Debug.Log("Je passe au Bleu");
            thisPoint = "PointBlue";
            Instantiate(blueLightPoint, thisCirclePosition, Quaternion.identity);
            AddScore(1);
        }

        else
        {
            //begin regen health
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        Debug.Log("Your score : " + score);
        scoreText.text = "Score: " + score;
    }
}