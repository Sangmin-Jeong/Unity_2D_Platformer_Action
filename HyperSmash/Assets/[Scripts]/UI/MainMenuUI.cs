////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: MainMenuUI.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 18/10/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button loadGameBtn;
    [SerializeField] private Button creditBtn;
    [SerializeField] private Button quitBtn;

    
    private void Start()
    {
        newGameBtn.onClick.AddListener(() =>
        {
            StartCoroutine(WaitForLoadScene("Main"));
        });
        
        loadGameBtn.onClick.AddListener(() =>
        {
            //TODO: Need save/load system
            //StartCoroutine(WaitForLoadScene(""));
        });
        
        creditBtn.onClick.AddListener(() =>
        {
            
        });
        
        quitBtn.onClick.AddListener(() =>
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        });
    }


    IEnumerator WaitForLoadScene(string SceneName)
    {
        yield return new WaitForSeconds(0.3f);
        SceneLoader.Instance.LoadScene(SceneName);
    }
}
