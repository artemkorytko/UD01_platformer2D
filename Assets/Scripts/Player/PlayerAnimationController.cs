using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private const string _speedKey = "SpeedInt";
    private const string _jumpKey = "Jump";
    private const string _deadKey = "Dead";
    private Animator _animator = null;

    protected void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeedDirection(int value)
    {
        _animator.SetInteger(_speedKey, value);
    }

    public void SetJump()
    {
        _animator.SetTrigger(_jumpKey);
    }

    public void SetDead()
    {
        _animator.SetBool(_deadKey, true);
    }
}
