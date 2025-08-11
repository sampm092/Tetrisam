using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOScript : MonoBehaviour
{
    public void RetryGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ExitGame()
     {
        Application.Quit();
        Debug.Log("Game Quit"); // Only shows in editor
    }
}
