using System;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private const string SPEED_KEY = "Speed";
    private const string VERTICAL_SPEED_KEY = "VerticalSpeed";
    private const string JUMP_KEY = "Jump";
    private const string GROUNDED_KEY = "Grounded";

    private static readonly int Speed = Animator.StringToHash(SPEED_KEY);
    private static readonly int VerticalSpeed = Animator.StringToHash(VERTICAL_SPEED_KEY);
    private static readonly int Jump = Animator.StringToHash(JUMP_KEY);
    private static readonly int Grounded = Animator.StringToHash(GROUNDED_KEY);
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(int value)
    {
        _animator.SetInteger(SPEED_KEY, value);
    }

    public void SetGrounded(bool value)
    {
        _animator.SetBool(GROUNDED_KEY, value);
    }
    
    public void JumpTrigger()
    {
        _animator.SetTrigger(JUMP_KEY);
        Debug.Log("True");
    }
    
    public void SetVerticalSpeed(float value)
    {
        _animator.SetFloat(VERTICAL_SPEED_KEY, value);
    }
}
