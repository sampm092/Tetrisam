using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour //For Game Over Scene
{
    public TextMeshProUGUI finalScore;
    public GameObject newHigh;
    public int score;

    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        checkHighscore(score);
        finalScoreShow();
    }

    public void finalScoreShow()
    {
        finalScore.text = score.ToString();
    }

    public void checkHighscore(int value)
    {
        if (
            value > PlayerPrefs.GetInt("highscore1")
            || value > PlayerPrefs.GetInt("highscore2")
            || value > PlayerPrefs.GetInt("highscore3")
            || value > PlayerPrefs.GetInt("highscore4")
        )
        {
            newHigh.SetActive(true);
        }
    }
}
