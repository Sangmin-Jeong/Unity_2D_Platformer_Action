////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: Character.cs
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
using UnityEngine.UI;
using DG.Tweening;

public class Character : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2D;
    
    // Stats
    [Header("Character Stats")] 
    [SerializeField] protected int _life;
    [SerializeField] protected float _health;
    [SerializeField] protected float _damage;
    public float _MAX_Health;
    protected bool _isDead = false;
    protected Vector3 _spawnPos;
    
    // Movement
    [Header("Movement")]
    [SerializeField] protected float _walkSpeed;
    [SerializeField] protected float _maxSpeed;
    [SerializeField] protected float _jumpForce;
    [SerializeField][Range(0.0f, 2.0f)]protected float _thresholdForJump = 1.0f;
    protected float _airFactor = 0.3f;
    [SerializeField] protected Transform _groundPoint;
    [SerializeField] protected LayerMask _groundingLayer;
    [SerializeField] protected bool _isGrounded = false;
    
    // Attack
    [HideInInspector] public bool _canAttack = true;
    [HideInInspector] public bool _canMove = true;
    
    // UI
    [HideInInspector]public Slider _healthSlider;
    
    // Sprite
    protected SpriteRenderer _spriteRenderer;
    
    // Knockback
    [HideInInspector] public Knockback _knockback;

    // Combo Attack
    [SerializeField]protected List<AttackSO> _combo;
    protected float _lastAttackTime;
    protected float _lastComboEnd;
    protected int _comboCounter;
    
    // Audio
    protected PlayerAudio _playerAudio;
    
    // Event
    public EventHandler<LifeChangedEventArgs> OnLifeChanged;
    public class LifeChangedEventArgs : EventArgs
    {
        public int _life;
    }

    private void Awake()
    {
        _spawnPos = transform.position;
    }

    public int GetLife()
    {
        return _life;
    }

    public void DecreaseLife()
    {
        _playerAudio.PlayDeathSFX();
        _life--;
        transform.position = _spawnPos;
        _health = _MAX_Health;
        if (_life <= 0)
        {
            _health = 0;
            _isDead = true;
            transform.DOScale(new Vector3(0, 0, 1), 2f).OnComplete(() => { SceneLoader.Instance.LoadScene("GameOverScene"); });
        }
        
        OnLifeChanged?.Invoke(this, new LifeChangedEventArgs{_life = _life});
    }
    
    public void IncreaseMaxHealth()
    {
        _MAX_Health += _MAX_Health * 0.3f;
    }
    
    public void GetHeal(float degree)
    {
        
        if ((_health + _MAX_Health * degree) > _MAX_Health)
        {
            _health = _MAX_Health;
        }
        else
        {
            _health += _MAX_Health * degree;
        }
        
        _healthSlider.value = _health / _MAX_Health;
    }
    
    
}
