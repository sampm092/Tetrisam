using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scores;
    void Start()
    {
        scores.text = PlayerPrefs.GetInt("highscores").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
