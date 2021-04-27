using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator _animator;
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        WalkAnimation();
        RollingAnimation();
    }

    private void WalkAnimation()
    {
        if (_playerMovement.direction.magnitude != 0)
            _animator.SetBool("Walk_Anim", true);
        else
            _animator.SetBool("Walk_Anim", false);
    }

    private void RollingAnimation()
    {
        if (_playerMovement.RollState())
        {
            if (_playerMovement.direction.magnitude != 0)
                _animator.SetBool("Roll_Anim", true);
        }
        else
        {    
            if (_playerMovement.direction.magnitude == 0)
            {
                _animator.SetBool("Roll_Anim", false);
                _animator.SetBool("Idle_Anim", true);
            }
            else
            {
                _animator.SetBool("Roll_Anim", false);
                _animator.SetBool("Idle_Anim", false);
            }
        }
    }
}