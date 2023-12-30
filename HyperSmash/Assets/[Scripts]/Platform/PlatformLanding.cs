////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlatformLanding.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 19/11/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLanding : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.parent = transform;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.transform.SetParent(null);
    }
}
