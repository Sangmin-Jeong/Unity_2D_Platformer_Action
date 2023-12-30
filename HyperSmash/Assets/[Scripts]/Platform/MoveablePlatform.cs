////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: MainUI.cs
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

public enum PlatformerMovementTypes
{
    HORIZONTAL,
    VERTICAL,
    DIAGONAL_RIGHT,
    DIAGONAL_LEFT,
    CUSTOM,
    ROTATE,
    NONE,
}

public class MoveablePlatform : MonoBehaviour
{
    [SerializeField] private PlatformerMovementTypes _type;
    [SerializeField] private float _horizontalSpeed = 5;
    [SerializeField] private float _horizontalLength = 5;
    [SerializeField] private float _verticalSpeed = 5;
    [SerializeField] private float _verticalLength = 5;
    
    [Header("Custom Movement")]
    [SerializeField] private List<Transform> _pathTransforms = new List<Transform>();
    private List<Vector2> _customMovementTargets = new List<Vector2>();
    private Vector3 _startPos;
    private Vector3 _endPos;

    private float _timer;
    [SerializeField] [Range(0f,.1f)]private float _timerSpeed;
    private int _currentTargetPathIndex;
    
    void Start()
    {
        _startPos = transform.position;

        foreach (Transform t in _pathTransforms)
        {
            _customMovementTargets.Add(t.position);
        }
        _customMovementTargets.Add(_startPos);
        _endPos = _customMovementTargets[_currentTargetPathIndex];
    }

    
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (_type == PlatformerMovementTypes.CUSTOM)
        {
            // 
            if (_timer < 1) // Less than 1 means that the platform doesn't reach to the end point
            {
                _timer += _timerSpeed;
            }
            else if (_timer >= 1) // Reached to the end Point so reset the Timer
            {
                _timer = 0;
                
                // Move to the next custom point
                _currentTargetPathIndex++;
                
                if (_currentTargetPathIndex >= _customMovementTargets.Count)
                {
                    _currentTargetPathIndex = 0;
                }

                _startPos = transform.position;
                _endPos = _customMovementTargets[_currentTargetPathIndex];
            }
            
        }
    }

    void Move()
    {
        switch (_type)
        {
            case PlatformerMovementTypes.HORIZONTAL:
                transform.position = new Vector2(
                    Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalLength) + _startPos.x,
                    transform.position.y);
                break;
            case PlatformerMovementTypes.VERTICAL:
                transform.position = new Vector2(transform.position.x,
                    Mathf.PingPong(_verticalSpeed * Time.time, _verticalLength) + _startPos.y);
                break;
            case PlatformerMovementTypes.DIAGONAL_RIGHT:
                transform.position = new Vector2(
                    Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalLength) + _startPos.x,
                    Mathf.PingPong(_verticalSpeed * Time.time, _verticalLength) + _startPos.y);
                break;
            case PlatformerMovementTypes.DIAGONAL_LEFT:
                transform.position = new Vector2(
                    _startPos.x - Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalLength),
                    Mathf.PingPong(_verticalSpeed * Time.time, _verticalLength) + _startPos.y);
                break;
            case PlatformerMovementTypes.CUSTOM:
                transform.position = Vector2.Lerp(_startPos, _endPos,_timer);
                break;
            case PlatformerMovementTypes.ROTATE:
                transform.Rotate(new Vector3(0,0,180) * Time.deltaTime);
                break;
            default:
                break;
        }
    }
}
