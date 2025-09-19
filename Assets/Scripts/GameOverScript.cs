using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI finalScore;
    public int score;

    // public GameScript GScript;

    void Start()
    {
        finalScoreShow();
        // GScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>();
    }

    public void finalScoreShow()
    {
        score = PlayerPrefs.GetInt("score");
        finalScore.text = score.ToString();
    }
}
