using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
  
   private const string SpeedKey = "SpeedInt";
   private const string JumpKey = "Jump";
   private static readonly int SpeedInt = Animator.StringToHash(SpeedKey);
   private static readonly int Jump = Animator.StringToHash(JumpKey);
   private Animator animator;
   
   private void Awake()
   {
      animator = GetComponentInChildren<Animator>();
   }

   public void SetSpeed(int value)
   {
      animator.SetInteger(SpeedInt,value);
   }
   
   public void SetJump()
   {
      animator.SetTrigger(Jump);
   }
}
