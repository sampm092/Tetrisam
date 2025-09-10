using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scores;

    void Start()
    {
        // PlayerPrefs.SetInt("highscores", 20000);
        scores.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    // Update is called once per frame
}
