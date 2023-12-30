////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: EnemySpawner.cs
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
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    
    [SerializeField] private GameObject _enemyPrefab;
    public List<GameObject> _enemyPool = new List<GameObject>();
    [SerializeField] private int _enemyTotal;
    [SerializeField] private float _spawnCount;
    private float _MAX_SpawnCount;
    private bool isAllDead = false;
    
    void Start()
    {
        Instance = this;
        BuildEnemyPool();
        _MAX_SpawnCount = _spawnCount;
    }
    
    void Update()
    {
        _spawnCount -= Time.deltaTime;
        if (_spawnCount < 0)
        {
            SpawnEnemy();
            _spawnCount = _MAX_SpawnCount;
        }

        foreach (GameObject enemy in _enemyPool)
        {
            if (!enemy.activeSelf)
            {
                isAllDead = true;
            }
            else
            {
                isAllDead = false;
                break;
            }
        }
        
        if(isAllDead)
            SpawnEnemy();
    }

    void BuildEnemyPool()
    {
        for (int i = 0; i < _enemyTotal; i++)
        {
            CreateEnemy();
        }
    }

    void CreateEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab);
        enemy.SetActive(false);
        enemy.transform.position = new Vector3(Random.Range(-20,20), 6, 1);
        enemy.transform.parent = transform;
        
        _enemyPool.Add(enemy);
    }

    public void SpawnEnemy()
    {
        if (_enemyPool.Count < 1)
        {
            CreateEnemy();
        }
        
        _enemyPool[Random.Range(0, _enemyPool.Count)].gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _enemyPool.Clear();
    }
}
