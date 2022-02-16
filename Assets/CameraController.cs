using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
   private CinemachineVirtualCamera camera;
   private Transform target;

   public void Initialize(Transform target)
   {
      this.target = target;
      camera.Follow = target;
      camera.LookAt = target;

   }

   private void Awake()
   {
      camera = GetComponent<CinemachineVirtualCamera>();
   }
}
