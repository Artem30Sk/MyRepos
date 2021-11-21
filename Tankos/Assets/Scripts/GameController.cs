using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //Работа со сценами

public class GameController : MonoBehaviour
{
    public int score = 0;
    public int cul;
    public GameObject Score;
    Text scoreText;
    private void Start()
    {
        
        string lname = SceneManager.GetActiveScene().name;
        if (lname == "FirstLevel")
        {
            score = 0;
            scoreText = Score.GetComponent<Text>();
            scoreText.text = score.ToString();
            scoreText.text = "Очки: " + score;
        }
        if (lname == "SecondLevel")
        {
            score = 60;
            scoreText = Score.GetComponent<Text>();
            scoreText.text = score.ToString();
            scoreText.text = "Очки: " + score;
        }
        if(lname == "ThirdLevel")
        {
            score = 130;
            scoreText = Score.GetComponent<Text>();
            scoreText.text = score.ToString();
            scoreText.text = "Очки: " + score;
        }
    }
    public void IncreaseScore(int increase)
    {
        score += increase;
        scoreText.text = score.ToString();
        scoreText.text = "Очки: " + score;
        

    }
}
