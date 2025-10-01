using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public GameScript GScript;
    public GameObject Settings;
    private AudioSource Sonsistem;
    public AudioClip tap;
    bool status = false;

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

    public void toSettings()
    {
        tapSound();
        status = !status;
        Settings.SetActive(status);
    }

    public void ExitGame()
    {
        Application.Quit();
        Screen.fullScreen = true;
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Debug.Log("Game Quit"); // Only shows in editor
    }

    public void toScore()
    {
        tapSound();
        SceneManager.LoadScene("Score");
    }

    public void tapSound()
    {
        if (Sonsistem != null && tap != null)
        {
            Sonsistem.PlayOneShot(tap);
        }
    }
}
