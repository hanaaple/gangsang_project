using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sfxSlider;
    public Slider bgmSlider;

    public AudioSource Sfx;
    public AudioSource Bgm;
    public AudioSource BgmOneShot;
    public AudioMixer audioMixer;

    public static SoundManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(value =>
        {
            audioMixer.SetFloat("Volume", (value <= volumeSlider.minValue) ? -80f : volumeSlider.value);
        });
        
        sfxSlider.onValueChanged.AddListener(value =>
        {
            audioMixer.SetFloat("Sfx", (value <= sfxSlider.minValue) ? -80f : sfxSlider.value);
        });
        
        bgmSlider.onValueChanged.AddListener(value =>
        {
            audioMixer.SetFloat("Bgm", (value <= bgmSlider.minValue) ? -80f : bgmSlider.value);
        });
    }


    public void PlaySfx(AudioClip audioClip)
    {
        Sfx.PlayOneShot(audioClip);
    }

    private bool isPaused = false;
    
    public void PlayBgm(AudioClip audioClip)
    {
        if (Bgm.clip != audioClip)
        {
            Bgm.clip = audioClip;   
        }
        if (isPaused)
        {
            Bgm.UnPause();   
        }
        else
        {
            Bgm.Play();   
        }
    }
    public void PlayBgmOneShot(AudioClip[] audioClip)
    {
        foreach (var clip in audioClip)
        {
            BgmOneShot.PlayOneShot(clip);   
        }
    }

    public void StopBgmOneShot()
    {
        BgmOneShot.Stop();
    }
    public void PauseBgm()
    {
        isPaused = true;
        Bgm.Pause();
    }
    public void StopBgm()
    {
        isPaused = false;
        Bgm.clip = null; 
        Bgm.Stop();
    }
}
