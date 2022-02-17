using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform mainPosition;
    private Transform _target;
    private CinemachineVirtualCamera _camera;
    
    public void Initialize(Transform target)
    {
        _target = target;
        _camera.Follow = _target;
        _camera.LookAt = _target;
    }

    public void Deactivate()
    {
        _camera.Follow = mainPosition;
        _camera.LookAt = mainPosition;
    }

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }
}
