using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenFix : MonoBehaviour
{
    void Start(){
        if (Application.platform == RuntimePlatform.Android)
        {
            Camera camera = Camera.main;
            Rect rect = camera.rect;
            float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16); // (가로 / 세로)
            float scalewidth = 1f / scaleheight;
            if (scaleheight < 1)
            {
                rect.height = scaleheight;
                rect.y = (1f - scaleheight) / 2f;
            }
            else
            {
                rect.width = scalewidth;
                rect.x = (1f - scalewidth) / 2f;
            }
            camera.rect = rect;
        }
        else
        {
            int width = Screen.width;
            int height = Screen.height;

            if (height > Screen.currentResolution.height)
            {
                height = (int) (Screen.currentResolution.height * 0.8f);
            }

            if (width > Screen.currentResolution.width)
            {
                width = (int) (Screen.currentResolution.width * 0.8f);
            }

            float ratio = width / (float) height;
            if (ratio > 9 / 16f)
            {
                Screen.SetResolution(height / 16 * 9, height, false, 0);
            }
            else
            {
                Screen.SetResolution(width, width / 9 * 16, false, 0);
            }
        }
    }

    private void OnPreCull()
    {
        GL.Clear(true, true, Color.black);
    }

    // private readonly int _width = 1080;
    // private readonly int _height = 1920;

    // private int _lastWidth;
    // private int _lastHeight;

    // private void Start()
    // {
    //     _lastWidth = Screen.width;
    //     _lastHeight = Screen.height;
    // }

    // void Update ()
    // {
    //     int width = Screen.width;
    //     int height = Screen.height;
    //     
    //     Debug.Log(Screen.width + "   " + Screen.height);
    //     
    //     if(_lastWidth != width) // if the user is changing the width
    //     {
    //         // update the height
    //         float heightAccordingToWidth = width / (float)_width * _height;
    //         Screen.SetResolution(width, Mathf.RoundToInt(heightAccordingToWidth), false, 0);
    //     }
    //     else if(_lastHeight != height) // if the user is changing the height
    //     {
    //         // update the width
    //         float widthAccordingToHeight = height / (float)_height * _width;
    //         Screen.SetResolution(Mathf.RoundToInt(widthAccordingToHeight), height, false, 0);
    //     }
    //
    //     Debug.Log(Screen.width + "   " + Screen.height);
    //     _lastWidth = width;
    //     _lastHeight = height;
    // }
}
