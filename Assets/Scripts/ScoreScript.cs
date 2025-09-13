using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts; // assign in Inspector (size = 4, drag your TMP objects here) : scores, scores2-4
    public GameObject ResetVer;
    private bool isActive = false;

    void Start()
    {
        LoadScores();
    }

    public void resetScore()
    {
        for (int i = 0; i < scoreTexts.Length; i++) //scoreTexts.Length = 4
        {
            PlayerPrefs.SetInt($"highscore{i + 1}", 0); // reset dynamically, highscore1-4
        }
        LoadScores();
    }

    public void toResetVer()
    {
        isActive = !isActive;
        ResetVer.SetActive(isActive);
    }

    private void LoadScores()
    {
        for (int i = 0; i < scoreTexts.Length; i++) //scoreTexts.Length = 4
        {
            int score = PlayerPrefs.GetInt($"highscore{i + 1}", 0); // default = 1,  highscore1-4
            scoreTexts[i].text = score.ToString();
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
