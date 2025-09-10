using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scores;

    void Start()
    {
        scores.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void resetScore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        scores.text = PlayerPrefs.GetInt("highscore").ToString();
    }
}
