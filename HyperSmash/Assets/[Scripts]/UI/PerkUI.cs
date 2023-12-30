////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PerkUI.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 18/10/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkUI : MonoBehaviour
{
    [SerializeField] private Button btn1;
    [SerializeField] private Button btn2;
    [SerializeField] private Button btn3;

    private PlayerController _player;

    // private void Awake()
    // {
    //
    // }
    //
    // void Start()
    // {
    //     _player = FindObjectOfType<PlayerController>();
    //     
    //     btn1.onClick.AddListener(() =>
    //     {
    //         _player.PowerUpForSeconds(10f);
    //         SoundManager.Instance.PlayPerkSFX();
    //         Time.timeScale = 1;
    //         Hide();
    //     });
    //     btn2.onClick.AddListener(() =>
    //     {
    //         _player.IncreaseMaxHealth();
    //         SoundManager.Instance.PlayPerkSFX();
    //         Time.timeScale = 1;
    //         Hide();
    //     });
    //     btn3.onClick.AddListener(() =>
    //     {
    //         _player.GetHeal();
    //         SoundManager.Instance.PlayPerkSFX();
    //         Time.timeScale = 1;
    //         Hide();
    //     });
    // }
    //
    //
    //
    // public void Show()
    // {
    //     gameObject.SetActive(true);
    //     // for (int i = 0; i < transform.childCount; i++)
    //     // {
    //     //     transform.GetChild(i).gameObject.SetActive(true);
    //     // }
    // }
    //
    // public void Hide()
    // {
    //     gameObject.SetActive(false);
    //     // for (int i = 0; i < transform.childCount; i++)
    //     // {
    //     //     transform.GetChild(i).gameObject.SetActive(false);
    //     // }
    // }
    //
    // IEnumerator WaitForHide()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     Time.timeScale = 1;
    //     Hide();
    // }
    //
}
