using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMScript : MonoBehaviour
{
    private static BGMScript instance;
    private AudioSource audioSource;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider volumeSlider;

    void Awake()
    // Awake() is called before Start(), as soon as the object is created.
    {
        if (instance == null) //The first time a BGMScript spawns, instance is null, so this object becomes the “official” instance.
        {
            instance = this; //assign this crip
            DontDestroyOnLoad(gameObject); // keep this across scenes even after loading other gameobjects
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // destroy other gameObject (another bgm) if theres another instance
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level")
        {
            audioSource.Stop(); // Stop in game scene
        }
        else if (scene.name == "Menu" || scene.name == "Score")
        {
            audioSource.Play(); // Resume in menus
        }
    }

    void OnDestroy()
    {
        // Prevent duplicate subscriptions
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void setMusicVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }
}
