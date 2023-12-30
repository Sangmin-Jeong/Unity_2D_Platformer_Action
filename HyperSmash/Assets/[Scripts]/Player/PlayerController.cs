////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlayerController.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 19/11/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class PlayerController : Character
{
    [HideInInspector]public PlayerInputAction _inputAction;
    
    // Movement
    [SerializeField]private Joystick _leftJoystick;
    
    // Attack
    public List<GameObject> _enemies = new List<GameObject>();

    
    // Animation
    [HideInInspector]public PlayerAnimController _playerAnimController;
    
    // Events
    public EventHandler OnHealthChanged;
    public EventHandler OnSelectPerk;
    
    private void Awake()
    {
        // Input Actions
        _inputAction = new PlayerInputAction();
        _inputAction.Enable();
        
         _inputAction.Player.Attack.performed += Attack;
    }

    private void Start()
    {
        // Initialize components
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _healthSlider = GetComponentInChildren<Slider>();
        _knockback = GetComponent<Knockback>();
        _MAX_Health = _health;
        _healthSlider.value = _health / _MAX_Health;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _thresholdForJump = 0.3f;
        _playerAudio = GetComponent<PlayerAudio>();
        _playerAnimController = GetComponentInChildren<PlayerAnimController>();
        GameInstance.Instance._score = 0;
        
        // Events
        OnHealthChanged += PlayerController_OnHealthChanged;
    }

    private void PlayerController_OnHealthChanged(object sender, EventArgs e)
    {
        
    }

    // private void OptionKey(InputAction.CallbackContext obj)
    // {
    // }

    private void Attack(InputAction.CallbackContext obj)
    {
        // Check if combo has ended, and certain amount of time(0.5f) has passed
        if (Time.time - _lastComboEnd > 0.8f && _comboCounter <= _combo.Count - 1)
        {
            // If there is a waiting invoke, cancel it.
            CancelInvoke("EndCombo");
            
            // If attack has pressed within certain amount of time(0.4f), perform the next attack.
            if (Time.time - _lastAttackTime >= 0.8f)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _playerAnimController.StopWalkAnim();
                // Get the next anim
                _playerAnimController.GetAnimator().runtimeAnimatorController =
                    _combo[_comboCounter]._animatorOV;

                _playerAudio.PlayAttackSFX(_comboCounter);
                _playerAnimController.PlayAttackAnim();
                
                // Apply damage and knockback
                foreach (GameObject enemy in _enemies)
                {
                    enemy.GetComponent<EnemyController>().TakeDamage(gameObject, _damage);
                    if (_comboCounter == 2)
                    {
                        _knockback.ApplyKnockback(enemy);
                    }
                    
                    // Increase Score
                    GameInstance.Instance._score += 300;
                    
                    if (_enemies.Count < 1) return;
                }
                
                // Prepare the next attack
                _comboCounter++;
                _lastAttackTime = Time.time;
                if (_comboCounter > _combo.Count - 1) _comboCounter = 0;
            }
        }
    }

    void ExitAttack()
    {
        // Keep checking if Combo is done
        if (_playerAnimController.GetAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f &&
            _playerAnimController.GetAnimator().GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo",0.8f);
        }
    }

    void EndCombo()
    {
        // Reset values for combo
        _playerAnimController.PlayIdleAnim();
        _canAttack = true;
        _canMove = true; 
        _comboCounter = 0;
        _lastComboEnd = Time.time;
    }
    
    void Update()
    {
        if (_isDead) return;
        
        IsGrounded();
        DebugInput();
        ExitAttack();
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Movement();
            Jump();
        }
    }

    private void Movement()
    {
        float leftJoystickInput = 0;
        if (_leftJoystick)
        {
            leftJoystickInput = _leftJoystick.Horizontal;
        }
        float XDirection = Input.GetAxisRaw("Horizontal") + leftJoystickInput;
        
        // 1 = right , -1 = left
        if (XDirection != 0)
        {
            Flip(XDirection);
        }
        
        Vector2 force = Vector2.zero;
        if (XDirection > 0)
        {
            force = Vector2.right * _walkSpeed * Time.deltaTime;
            _rigidbody2D.AddForce(force);
        }
        else if (XDirection < 0)
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

        if (Mathf.Abs(_rigidbody2D.velocity.x) > 1f)
        {
            _playerAnimController.PlayWalkAnim();
        }
        else
        {
            _playerAnimController.PlayIdleAnim();
        }
    }
    
    void Jump()
    {
        float leftJoystickInput = 0;
        
        if (_leftJoystick)
        {
            leftJoystickInput = _leftJoystick.Vertical;
        }
        float _isJumping = Input.GetAxisRaw("Jump") + leftJoystickInput;
        
        if(_isGrounded && _isJumping > _thresholdForJump)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
            //SoundManager.Instance.PlaySound(Channel.PLAYER_SFX, Sound.JUMP);
        }
    }
    
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(_groundPoint.position, 
            0.1f, 
            Vector2.down,
            0.1f,
            _groundingLayer);
        _isGrounded = hit;
        return hit;
    }
    
    void Flip(float direction)
    {
        if (direction > 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        
    }

    private void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneLoader.Instance.LoadScene("GameOverScene");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameInstance.Instance._winFactor = GameInstance.Instance._winCondition;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetHeal(1f);
        }
    }

    public void TakeDamage(GameObject causer, float damage)
    {
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        
        _playerAudio.PlayHitSFX();
        //_knockback.ApplyKnockback(causer);
        
        // Hit Animation
        if(!DOTween.IsTweening(_spriteRenderer))
        _spriteRenderer.DOColor(Color.black, 0.1f).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
        {
            _spriteRenderer.color = Color.white;
        });
        
        _health -= damage;
        if (_health <= 0)
        {
            // Dead
            DecreaseLife();
        }
        
        _healthSlider.value = _health / _MAX_Health;
        //Debug.Log(_health);
    }

    private void SetDead()
    {
        _playerAnimController.DisableAnimator();
        _inputAction.Disable();
        _health = 0;
        _isDead = true;
        transform.DOScale(new Vector3(0, 0, 1), 2f).OnComplete(() => { SceneLoader.Instance.LoadScene("GameOverScene"); });
    }

    public bool GetIsDead()
    {
        return _isDead;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _enemies.Add(other.gameObject);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _enemies.Remove(other.gameObject);
        }
    }
    
    private void OnDestroy()
    {
        _enemies.Clear();
        DOTween.Clear();
    }
    
    public IEnumerator WaitForWin()
    {
        _inputAction.Disable();
        //_playerAnimController.DisableAnimator();
        SoundManager.Instance.PlayWinSFX();
        //_healthSlider.gameObject.SetActive(false);
        EnemySpawner.Instance.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(2f);
        
        SceneLoader.Instance.LoadScene("WinScene");
    }
    
    // IEnumerator WaitForPerk()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     OnSelectPerk?.Invoke(this, EventArgs.Empty);
    // }
}