////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: LifeCounter.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On : 19/11/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    [SerializeField] private List<GameObject> _images;
    private PlayerController _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _player.OnLifeChanged += PlayerController_OnLifeChanged;
        
        foreach (GameObject life in _images)
        {
            life.SetActive(false);
        }
        
        for (int i = 0; i < _player.GetLife(); i++)
        {
            _images[i].SetActive(true);
        }
    }

    private void PlayerController_OnLifeChanged(object sender, Character.LifeChangedEventArgs e)
    {
        foreach (GameObject life in _images)
        {
            life.SetActive(false);
        }

        for (int i = 0; i < e._life; i++)
        {
            _images[i].SetActive(true);
        }
    }


    void Update()
    {
        
    }
}
