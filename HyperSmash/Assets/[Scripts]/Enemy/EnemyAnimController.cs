////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: EnemyAnimController.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 19/11/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    private Animator _animator;

    private EnemyController _enemyController;
    // Start is called before the first frame update
    void Start()
    {
        _enemyController = transform.parent.GetComponent<EnemyController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Animator GetAnimator()
    {
        return _animator;
    }
    
    public void EnableCanMove()
    {
        _enemyController._canMove = true;
    }
    
    public void DisableCanMove()
    {
        _enemyController._canMove = false;
    }
    
    
    public void EnableCanAttack()
    {
        _enemyController._canAttack = true;
    }
    
    public void DisableCanAttack()
    {
        _enemyController._canAttack = false;
    }

    public void PlayIdleAnim()
    {
        _animator.SetBool("Idle" , true);
        _animator.SetBool("Walk", false);
    }
    
    public void PlayAttackAnim()
    {
        _animator.SetTrigger("Attack");
    }
    public void PlayWalkAnim()
    {
        _animator.SetBool("Walk", true);
        _animator.SetBool("Idle" , false);
    }
    public void StopWalkAnim()
    {
        _animator.SetBool("Walk", false);
        _animator.SetBool("Idle" , true);
    }

    public void DisableAnimator()
    {
        _animator.enabled = false;
    }
}