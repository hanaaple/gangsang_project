using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UnityAnimationEvent : UnityEvent<string>{};

[RequireComponent(typeof(Animator))]
public class AnimationEventDispatcher : MonoBehaviour
{
    public UnityAnimationEvent onAnimationComplete;

    public Animator animator;
    public AnimationClip targetClip;

    private bool _isActivated = false;

    void Awake()
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip != targetClip) continue;
            AnimationEvent animationEndEvent = new AnimationEvent
            {
                time = clip.length - 0.1f,
                functionName = "AnimationCompleteHandler",
                stringParameter = clip.name
            };
            clip.AddEvent(animationEndEvent);
        }
    }
    public void AnimationCompleteHandler(string clipName)
    {
        if (_isActivated) return;
        
        _isActivated = true;
        Debug.Log($"{clipName} animation complete.");
        onAnimationComplete?.Invoke(clipName);
    }
}