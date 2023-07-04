using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static int scoreValue;
    public Text Score;
    void Start()
    {
        scoreValue = 0;
        Score.text = "Score:" + scoreValue.ToString();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D FoodPrefab)
    {
        if (FoodPrefab.tag == "food")
        {
            scoreValue++;
            Score.text = "Score:" + scoreValue.ToString();

        }
    }
}