using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private const string _speedKey = "SpeedInt";
    private Animator _animator = null;

    protected void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void SetSpeedDirection(int value)
    {
        _animator.SetInteger(_speedKey, value);
    }
}
