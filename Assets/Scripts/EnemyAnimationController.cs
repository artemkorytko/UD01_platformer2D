using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private const string SPEED_KEY = "Speed";
    
    private static readonly int Speed = Animator.StringToHash(SPEED_KEY);
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    
    public void SetSpeed(int value)
    {
        _animator.SetInteger(SPEED_KEY, value);
    }
}
