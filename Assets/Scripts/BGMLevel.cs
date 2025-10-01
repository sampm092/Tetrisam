using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BGMLevel : MonoBehaviour //For BGM in Level Scene
{
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider volumeSlider;

    // Start is called before the first frame update
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

    // Update is called once per frame
    public void setMusicVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
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
