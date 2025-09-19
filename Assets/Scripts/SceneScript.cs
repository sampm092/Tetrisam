using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public GameScript GScript;
    private AudioSource Sonsistem;
    public AudioClip tap;

    void Start()
    {
        Sonsistem = FindObjectOfType<AudioSource>();
    }

    public void StartGame()
    {
        tapSound();
        SceneManager.LoadScene("Level");
    }

    public void RetryGame()
    {
        tapSound();
        SceneManager.LoadScene("Level");
    }

    public void ToMenuPause()
    {
        Time.timeScale = 1;
        GScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>();
        GScript.isPaused = !GScript.isPaused;
        tapSound();
        SceneManager.LoadScene("Menu");
    }

    public void ToMenu()
    {
        tapSound();
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // Only shows in editor
    }

    public void toScore()
    {
        tapSound();
        SceneManager.LoadScene("Score");
    }

    void tapSound()
    {
        if (Sonsistem != null && tap != null)
        {
            Sonsistem.PlayOneShot(tap);
        }
    }
}
