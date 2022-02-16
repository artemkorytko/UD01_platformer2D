using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private const string SpeedKey = "SpeedInt";
    private static readonly int SpeedInt = Animator.StringToHash(SpeedKey);
    private Animator animator;
   
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(int value)
    {
        animator.SetInteger(SpeedInt,value);
    }

}
