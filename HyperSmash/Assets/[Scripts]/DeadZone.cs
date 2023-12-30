////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: DeadZone.cs
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

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().DecreaseLife();
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().Respawn();
        }
        else if (other.CompareTag("Item"))
        {
            other.GetComponent<Item>().isSpawned = false;
        }
    }
}
