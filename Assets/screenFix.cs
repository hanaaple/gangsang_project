using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenFix : MonoBehaviour
{
    void Awake()
    {
        int width = 1080;
        int height = 1920;
        Screen.SetResolution(width, height, true);
    }
}
