////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: SoundManager.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 19/11/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get;  private set; }
    
    [SerializeField] private AudioSource CameraAudioSource;
    [SerializeField] private AudioSource WinAudioSource;
    //[SerializeField] private AudioSource PerkAudioSource;
    [SerializeField] public AudioClip audioClipForMainMenuScene;
    [SerializeField] public AudioClip audioClipForMainScene;
    [SerializeField] public AudioClip audioClipForGameOverScene;
    [SerializeField] public AudioClip audioClipForWinScene;
    
    //SFX
    [SerializeField] private AudioClip winSFX;

    
    private void Awake()
    {
        Instance = this;
        CameraAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    private void Start()
    {
        switch (SceneLoader.Instance.GetCurrentSceneName())
        {
            case "MainMenu":
                ChangeMusic(audioClipForMainMenuScene);
                break;
            case "Main":
                ChangeMusic(audioClipForMainScene);
                break;
            case "GameOverScene":
                ChangeMusic(audioClipForGameOverScene);
                break;
            case "WinScene":
                ChangeMusic(audioClipForWinScene);
                break;
        }
    }

    public void PlayBGM()
    {
        CameraAudioSource?.Play();
    }

    public void StopMusic()
    {
        CameraAudioSource?.Stop();
    }

    public void ChangeMusic(AudioClip audioClip)
    {
        if (CameraAudioSource.clip != audioClip)
        {
            CameraAudioSource.Stop();
            CameraAudioSource.clip = audioClip;
            CameraAudioSource.loop = true;
            CameraAudioSource.volume = 0.3f;
            CameraAudioSource.Play();
        }
    }

    public bool IsPlaying()
    {
        return CameraAudioSource.isPlaying;
    }

    public AudioSource GetAudioSource()
    {
        return CameraAudioSource;
    }

    public void PlayWinSFX()
    {
        WinAudioSource.clip = winSFX;
        WinAudioSource.Play();
    }
    // public void PlayPerkSFX()
    // {
    //     PerkAudioSource.Play();
    // }
    
}