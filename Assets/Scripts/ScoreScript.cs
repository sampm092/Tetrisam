using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scores;
    public TextMeshProUGUI scores2;
    public TextMeshProUGUI scores3;
    public TextMeshProUGUI scores4;

    void Start()
    {
        scores.text = PlayerPrefs.GetInt("highscore").ToString();
        scores2.text = PlayerPrefs.GetInt("highscore2").ToString();
        scores3.text = PlayerPrefs.GetInt("highscore3").ToString();
        scores4.text = PlayerPrefs.GetInt("highscore4").ToString();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void resetScore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.SetInt("highscore2", 0);
        PlayerPrefs.SetInt("highscore3", 0);
        PlayerPrefs.SetInt("highscore4", 0);
        scores.text = PlayerPrefs.GetInt("highscore").ToString();
        scores2.text = PlayerPrefs.GetInt("highscore2").ToString();
        scores3.text = PlayerPrefs.GetInt("highscore3").ToString();
        scores4.text = PlayerPrefs.GetInt("highscore4").ToString();
    }
}
