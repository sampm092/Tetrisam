using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSliderScript : MonoBehaviour
{
    public Slider screenSlide;

    void Start()
    {
        screenSlidePos();
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
}
