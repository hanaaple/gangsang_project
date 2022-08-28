using System;
using UnityEngine;
using UnityEngine.UI;

public class ActivityButton : MonoBehaviour
{
    public ActivityType activityType; 
    
    [SerializeField]
    private Text priceText;

    public Button activityButton;

    public void Initialize()
    {
        
        activityButton.onClick.AddListener(() =>
        {
            Activity.instance.Activate(activityType);
        });
        priceText.text = ((int)Activity.instance.GetActivity(activityType).credit).ToString();
    }
}
