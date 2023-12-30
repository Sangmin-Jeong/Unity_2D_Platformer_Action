////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: Item.cs
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
using Random = UnityEngine.Random;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemSO _itemSO;
    [HideInInspector] public bool isSpawned = false;
    private float _spawnTimer;
    [SerializeField] private float _spawnTime;
    [SerializeField] private Vector2 _spawnArea;
    [SerializeField] private AudioSource _usedSFX;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        Hide();
    }

    private void Update()
    {
        if(!isSpawned)
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer > _spawnTime)
        {
            if (!isSpawned)
            {
                Show();
                _spawnTimer = 0;
                isSpawned = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            _usedSFX.Play();
            _itemSO.UseItem(other.gameObject);
            Hide();
            isSpawned = false;
        }
    }

    private void Hide()
    {
        _spriteRenderer.enabled = false;
        _boxCollider.enabled = false;
    }
    
    private void Show()
    {
        float ranX = Random.Range(-_spawnArea.x , _spawnArea.x);
        float ranY = Random.Range(-_spawnArea.y , _spawnArea.y);
        transform.position = new Vector3(ranX, ranY, 1);
        _spriteRenderer.enabled = true;
        _boxCollider.enabled = true;
    }
}
