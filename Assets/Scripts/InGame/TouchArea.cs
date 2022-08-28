using UnityEngine;
using UnityEngine.EventSystems;

public class TouchArea : MonoBehaviour, IPointerDownHandler
{
    public CreditManager creditManager;
    public StageManager stageManager;

    public AudioClip coinAudio;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (stageManager.GetIsPlaying())
        {
            SoundManager.instance.PlaySfx(coinAudio);
            creditManager.EarnCreditPerTouch();
        }
    }
}
