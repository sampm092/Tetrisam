using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public GameScript GScript;
    public GameObject Settings;
    public Slider screenSlide;
    private AudioSource Sonsistem;
    public AudioClip tap;
    bool status = false;

    void Start()
    {
        Sonsistem = FindObjectOfType<AudioSource>();
        screenSlidePos();
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

    public void SetScreenMode()
    {
        int sliderValue = Mathf.RoundToInt(screenSlide.value);
        if (sliderValue == 0)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
            Debug.Log("Fullscreen");
        }
        else if (sliderValue == 1)
        {
            // Windowed (95% of full screen)
            int width = Mathf.RoundToInt(Display.main.systemWidth * 0.95f);
            int height = Mathf.RoundToInt(Display.main.systemHeight * 0.95f);

            Screen.SetResolution(width, height, FullScreenMode.Windowed);
            Screen.fullScreen = false;

            Debug.Log($"Windowed at {width}x{height}");
        }
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

    void screenSlidePos()
    {
        if (screenSlide != null)
        {
            screenSlide.wholeNumbers = true;
            screenSlide.minValue = 0;
            screenSlide.maxValue = 1;

            if (Screen.fullScreen)
            {
                screenSlide.value = 0; // fullscreen
            }
            else
            {
                screenSlide.value = 1; // windowed
            }
        }
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
