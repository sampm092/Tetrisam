using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;

    void Start()
    {
        LoadScores();
    }

    public void resetScore()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            PlayerPrefs.SetInt($"highscore{i + 1}", 0);
        }
        LoadScores();
    }

    private void LoadScores()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt($"highscore{i + 1}", 0);
            scoreTexts[i].text = score.ToString();
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
