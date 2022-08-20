using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activitys : MonoBehaviour
{
    public ActivityButton[] activityButtons;
    public StageManager stageManager;

    public void Start()
    {
        foreach (ActivityButton t in activityButtons)
        {
            t.Initialize();
        }
    }
}
