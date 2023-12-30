////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: GameOverUI.cs
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

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button playAgainBtn;
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Text ScoreText;

    
    private void Start()
    {
        ScoreText.text = GameInstance.Instance._score.ToString();
        
        playAgainBtn.onClick.AddListener(() =>
        {
            StartCoroutine(WaitForLoadScene("Main"));
        });
        
        mainMenuBtn.onClick.AddListener(() =>
        {
            StartCoroutine(WaitForLoadScene("MainMenu"));
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
