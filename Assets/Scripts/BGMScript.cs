using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMScript : MonoBehaviour // mostly for main scene BGM
{
    private static BGMScript instanceBGM;
    private AudioSource audioSource;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider volumeSlider;
    public bool toMenuStat = false;

    void Awake()
    // Awake() is called before Start(), as soon as the object is created.
    {
        if (instanceBGM == null) //The first time a BGMScript spawns, instance is null, so this object becomes the “official” instance.
        {
            instanceBGM = this; //assign this crip
            DontDestroyOnLoad(gameObject); // keep this across scenes even after loading other gameobjects
            audioSource = GetComponent<AudioSource>();

            SceneManager.sceneLoaded += OnSceneLoaded; //every time scene loaded, call onsceneloaded
        }
        else if (instanceBGM != this)
        {
            Destroy(gameObject); // destroy other gameObject (another bgm) if theres another instance
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlidePos();
        }
        else
        {
            setMusicVolume();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) //to choose what to do based on scene
    {
        if (scene.name == "Level")
        {
            audioSource.Stop(); // Stop in game scene
            toMenuStat = true;
        }
        else if (scene.name == "Menu")
        {
            if (!audioSource.isPlaying) //untuk score
                audioSource.Play(); // Resume in menus
            if (toMenuStat == true)
            {
                toMenuStat = false; //to specify this executed if player from level scene
                audioSource.Play();
            }
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
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20); // means: “Take my slider’s linear value (0–1), convert it into decibels so the AudioMixer understands it, and apply it to the exposed parameter called music.”
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    void musicSlidePos()
    {
        if (volumeSlider != null)
        {
            // Make sure the slider starts at the current volume
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f); // or load from PlayerPrefs / default

            // Subscribe to value change
            volumeSlider.onValueChanged.AddListener(value =>
            {
                PlayerPrefs.SetFloat("musicVolume", value);
                setMusicVolume(); // set volume value based on slider
            });
        }
    }
}
