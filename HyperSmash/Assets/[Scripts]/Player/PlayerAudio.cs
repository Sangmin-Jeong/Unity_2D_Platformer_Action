////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlayerAudio.cs
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

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource HitAudioSource;
    [SerializeField] private AudioSource AttackAudioSource;
    [SerializeField] private AudioSource DeathAudioSource;
    [SerializeField] private AudioClip HitSFX;
    [SerializeField] private AudioClip DeathSFX;
    [SerializeField] private List<AudioClip> AttackSFXs;

    private void Start()
    {
        HitAudioSource.clip = HitSFX;
        DeathAudioSource.clip = DeathSFX;
        AttackAudioSource.clip = AttackSFXs[0];
    }

    public void PlayHitSFX()
    {
        HitAudioSource.Play();
    }
    
    public void PlayDeathSFX()
    {
        DeathAudioSource.Play();
    }
    public void PlayAttackSFX(int idx)
    {
        AttackAudioSource.clip = AttackSFXs[idx];
        AttackAudioSource.Play();
    }
}
