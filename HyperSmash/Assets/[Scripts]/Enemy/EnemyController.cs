////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: EnemyController.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 19/11/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Character
{
    private GameObject _player;
    [SerializeField] private PlayerController _targetPlayer = null;
    
    [SerializeField] private Image _fill;
    
    // AI
    [Header("AI")]
    [SerializeField] private Transform _headPoint;
    [SerializeField] private Transform _frontStepPoint;
    [SerializeField] private float _groundRadius = 1f;
    private bool _isThereObstacleFront = false;
    private bool _isThereStepFront = false;
    private PlayerDetection _playerDetection;
    
    // Attack
    //[SerializeField] private float _cooldown = 1f;
    
    // Animation
    private EnemyAnimController _enemyAnimController;
    
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _healthSlider = GetComponentInChildren<Slider>();
        _knockback = GetComponent<Knockback>();
        _MAX_Health = _health;
        _healthSlider.value = _health / _MAX_Health;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerDetection = GetComponentInChildren<PlayerDetection>();
        _enemyAnimController = GetComponentInChildren<EnemyAnimController>();
        _playerAudio = GetComponent<PlayerAudio>();
        _player = GameObject.Find("Player").gameObject;
    }
    
    void Update()
    {
        if(_targetPlayer)
            if (_targetPlayer.GetIsDead()) return;
        
        IsGrounded();
        ExitAttack();
        
        if (_healthSlider)
        {
            _healthSlider.value = _health / _MAX_Health;
            //Attack();
            //Move();
            
            if (_healthSlider.value < 0.2f)
            {
                _fill.color = Color.red;
            }
            else
            {
                _fill.color = Color.yellow;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if(_targetPlayer)
        if (_targetPlayer.GetIsDead()) return;
        
        _isThereObstacleFront = Physics2D.Linecast(_groundPoint.position, _headPoint.position, _groundingLayer);
        _isThereStepFront = Physics2D.Linecast(_groundPoint.position, _frontStepPoint.position, _groundingLayer);
        _isGrounded = Physics2D.OverlapCircle(_groundPoint.position, _groundRadius, _groundingLayer);
        
        if (_isGrounded && (_isThereObstacleFront || !_isThereStepFront))
        {
            ChangeDirection();
        }

        if (_canMove)
        {
            if(_isGrounded && _playerDetection.CheckFollowDistance() && !_playerDetection.CheckPlayerFallingDown())
                Move();

            Jump();
        }

        
        if (_playerDetection.CheckAttackDistance())
        {
            Attack();
        }
    }
    

    
    public void ChangeDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
    }

    public void TakeDamage(GameObject causer, float damage)
    {
        //if (_health <= 0) return;
        
        _health -= damage;
        float healthPercent = _health / _MAX_Health;
        _healthSlider.value = healthPercent;
        
        _playerAudio.PlayHitSFX();
        //_knockback.ApplyKnockback(causer);
        
        // Hit Animation
        if(!DOTween.IsTweening(_spriteRenderer))
            _spriteRenderer.DOColor(Color.black, 0.1f).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
            {
                _spriteRenderer.color = Color.white;
            });


        
        if (_health <= 0)
        {
            Respawn();
        }
        
        
        //Debug.Log(_health);
    }

    public void Respawn()
    {
        //causer.GetComponent<PlayerController>()._enemies.Remove(gameObject);
        _playerAudio.PlayDeathSFX();
        GameInstance.Instance._winFactor++;
        transform.position = _spawnPos;
        _health = _MAX_Health;
        _healthSlider.value = 1.0f;
    }

    private void Attack()
    {
        // Check if combo has ended, and certain amount of time(0.5f) has passed
        if (Time.time - _lastComboEnd > 1.5f && _comboCounter <= _combo.Count - 1 && _targetPlayer)
        {
            // If there is a waiting invoke, cancel it.
            CancelInvoke("EndCombo");
            
            // If attack has pressed within certain amount of time(0.4f), perform the next attack.
            if (Time.time - _lastAttackTime >= 0.8f && _targetPlayer)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _enemyAnimController.StopWalkAnim();
                // Get the next anim
                _enemyAnimController.GetAnimator().runtimeAnimatorController =
                    _combo[_comboCounter]._animatorOV;
                
                _playerAudio.PlayAttackSFX(_comboCounter);
                _enemyAnimController.PlayAttackAnim();
                
                // Apply damage and knockback
                _targetPlayer.GetComponent<PlayerController>().TakeDamage(gameObject, _damage);
                if (_comboCounter == 2)
                {
                    _knockback.ApplyKnockback(_targetPlayer.gameObject);
                }
                
                // Prepare the next attack
                _comboCounter++;
                _lastAttackTime = Time.time;
                if (_comboCounter > _combo.Count - 1) _comboCounter = 0;
            }
        }
        else
        {
            _enemyAnimController.StopWalkAnim();
        }

    }
    
    void ExitAttack()
    {
        // Keep checking if Combo is done
        if (_enemyAnimController.GetAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f &&
            _enemyAnimController.GetAnimator().GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo",1);
        }
    }

    void EndCombo()
    {
        // Reset values for combo
        _enemyAnimController.PlayIdleAnim();
        _canAttack = true;
        _canMove = true; 
        _comboCounter = 0;
        _lastComboEnd = Time.time;
    }

    private void Move()
    {
        if (Mathf.Abs(_rigidbody2D.velocity.x) > 1f)
        {
            _enemyAnimController.PlayWalkAnim();
        }
        else
        {
            _enemyAnimController.PlayIdleAnim();
        }
        
        Vector2 force = Vector2.zero;
        if (_playerDetection._normalizedDirection.x > 0)
        {
            force = Vector2.right * _walkSpeed * Time.deltaTime;
            _rigidbody2D.AddForce(force);
        }
        else if (_playerDetection._normalizedDirection.x < 0)
        {
            force = Vector2.left * _walkSpeed * Time.deltaTime;
            _rigidbody2D.AddForce(force);
            
        }
        
        float maxSpeed = _maxSpeed;
        if (force != Vector2.zero)
        {
            if (!_isGrounded)
            {
                force = force * _airFactor;
                maxSpeed = _airFactor * _maxSpeed;
            }
                
        }
        _rigidbody2D.AddForce(force);
        _rigidbody2D.velocity = new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x, -_maxSpeed, maxSpeed),
            _rigidbody2D.velocity.y);
        
    }
    
    void Jump()
    {
        if (_playerDetection._normalizedDirection.y >= _thresholdForJump)
        {
            if(_isGrounded)
            {
                _rigidbody2D.AddForce(new Vector3(_playerDetection._normalizedDirection.x * _walkSpeed,_playerDetection._normalizedDirection.y * _jumpForce,0));
                
                //SoundManager.Instance.PlaySound(Channel.PLAYER_SFX, Sound.JUMP);
            }
        }

    }
    
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(_groundPoint.position, 
            0.7f, 
            Vector2.down,
            0.1f,
            _groundingLayer);
        _isGrounded = hit;
        return hit;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _canAttack = true;
            _targetPlayer = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _canAttack = false;
            _targetPlayer = null;
        }
    }
    
    private void OnDestroy()
    {
        DOTween.Clear();
    }
}
