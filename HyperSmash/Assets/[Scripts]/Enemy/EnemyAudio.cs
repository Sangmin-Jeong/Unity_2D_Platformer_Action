////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: EnemyAudio.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On : 18/10/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource HitAudioSource;
    [SerializeField] private AudioClip HitSFX;
    
    private void Start()
    {
        HitAudioSource.clip = HitSFX;
    }

    public void PlayHitSFX()
    {
        HitAudioSource.Play();
    }
}
