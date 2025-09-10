using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public GameScript GScript;

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ToMenu()
    {
        Time.timeScale = 1;
        GScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>();
        GScript.isPaused = !GScript.isPaused;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // Only shows in editor
    }

    public void toScore()
    {
        SceneManager.LoadScene("Score");
    }
}
