////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: SceneLoader.cs
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

public class SceneLoader : MonoBehaviour
{
    static public SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
