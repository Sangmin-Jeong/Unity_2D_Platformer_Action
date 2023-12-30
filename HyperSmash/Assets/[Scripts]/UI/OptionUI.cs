////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: OptionUI.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 19/11/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button SFX_Btn;
    [SerializeField] private Button BGS_Btn;
    [SerializeField] private Button QuitBtn;
    
    [SerializeField] private MainUI mainPanel;

    private void Start()
    {
        resumeBtn.onClick.AddListener(() =>
        {
            mainPanel.Show();
            Time.timeScale = 1;
            Hide();
        });
        
        SFX_Btn.onClick.AddListener(() =>
        {
            
        });

        BGS_Btn.onClick.AddListener(ToggleBGM);
        
        QuitBtn.onClick.AddListener(() =>
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        });
    }
    
    void ToggleBGM()
    {
        if (SoundManager.Instance.IsPlaying())
        {
            SoundManager.Instance.GetAudioSource().Pause();
        }
        else
        {
            SoundManager.Instance.GetAudioSource().Play();
        }
    }


    IEnumerator WaitForLoadScene(string SceneName)
    {
        yield return new WaitForSeconds(0.3f);
        SceneLoader.Instance.LoadScene(SceneName);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
