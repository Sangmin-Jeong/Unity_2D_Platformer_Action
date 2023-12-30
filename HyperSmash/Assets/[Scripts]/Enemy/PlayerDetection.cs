////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlayerDetection.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 19/11/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    //public bool _isSensingPlayer = false;
    public bool LOS = false;
    public Vector2 _normalizedDirection;
    public float _distance;
    public float _followFactor;
    public float _attackFactor;
    private PlayerController _player;
    private EnemyController _enemy;
    [SerializeField]private LayerMask _layerMask;
    
    void Start()
    {
        _enemy = transform.parent.GetComponent<EnemyController>();
        _player = FindObjectOfType<PlayerController>();
    }
    
    void Update()
    {
        if (_player.GetIsDead() || !_player) return;
        
        RaycastHit2D hit = Physics2D.Linecast(transform.position, _player.transform.position, _layerMask);
        _distance = hit.distance;
        _normalizedDirection = (_player.transform.position - transform.position).normalized;
        
        // + Right // - Left
        int playerDirection = (_normalizedDirection.x > 0) ? 1 : -1;
        int enemyDirection = Mathf.RoundToInt(transform.parent.transform.localScale.x);

        if (hit)
        {
            string colliderName = hit.collider.name;
            LOS = (colliderName == "Player") && (playerDirection == enemyDirection);
            if (!LOS && _distance != 0)
            {
                _enemy.ChangeDirection();
            }
        }
    }
    
    public bool CheckAttackDistance()
    {
        if (_distance < _attackFactor)
            return true;

        return false;
    }

    public bool CheckFollowDistance()
    {
        if (_distance > _followFactor)
            return true;

        return false;
    }
    
    public bool CheckPlayerFallingDown()
    {
        if (_player.transform.position.y < -3f)
            return true;

        return false;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         _isSensingPlayer = true;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         _isSensingPlayer = false;
    //     }
    // }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Color _color = (LOS) ? Color.green : Color.red;
        Debug.DrawLine(transform.position, _player.transform.position,_color);
    }
}
