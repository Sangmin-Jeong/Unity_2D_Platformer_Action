using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance;
    
    // Score
    public int _score;
    
    // Win Condition
    public int _winCondition;
    public int _winFactor;

    private PlayerController _player;

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

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (_winCondition <= _winFactor)
        {
            StartCoroutine(_player.WaitForWin());
            _winFactor = 0;
        }
            
    }
    

}
