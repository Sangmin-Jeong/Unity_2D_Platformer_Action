////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlayerAnimController.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 19/11/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator _animator;

    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = transform.parent.GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _animator.enabled = true;
    }

    public Animator GetAnimator()
    {
        return _animator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void EnableCanMove()
    {
        _playerController._canMove = true;
    }
    
    public void DisableCanMove()
    {
        _playerController._canMove = false;
    }
    
    
    public void EnableCanAttack()
    {
        _playerController._canAttack = true;
    }
    
    public void DisableCanAttack()
    {
        _playerController._canAttack = false;
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

    IEnumerator WaitForApply(string animName)
    {
        yield return new WaitForSeconds(0.2f);
        _animator.SetTrigger(animName);
    }
}