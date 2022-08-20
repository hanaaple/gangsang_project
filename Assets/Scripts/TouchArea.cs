using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchArea : MonoBehaviour, IPointerDownHandler
{
    public CreditManager creditManager;
    public StageManager stageManager;
    
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (stageManager.GetIsPlaying())
        {
            creditManager.EarnCreditPerTouch();
        }
    }
}
