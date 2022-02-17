using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;

    [SerializeField] private bool _isActive = true;

    private Transform _target;
    private EnemyAnimationController _animationController;

    private void Awake()
    {
        _target = Random.Range(0, 2) == 0 ? firstPoint : secondPoint;
        _animationController = GetComponentInChildren<EnemyAnimationController>();
    }

    private void Update()
    {
        if (!_isActive) return;
        
        var direction = (_target.position - transform.position).normalized;
        var moveDistance = speed * Time.deltaTime;
        var distanceToTarget = Vector3.Distance(_target.position, transform.position);

        if (moveDistance > distanceToTarget)
        {
            _target = _target == firstPoint ? secondPoint : firstPoint;
            moveDistance = distanceToTarget;
        }
        transform.Translate(direction * moveDistance);
        _animationController.SetSpeed((int) Mathf.Sign(direction.x));
        
        UpdateSide(-direction.x);
    }

    private void UpdateSide(float side)
    {
        var _side = Mathf.Sign(side);
        var localScale = transform.localScale;
        if (Mathf.Sign(localScale.x) != _side)
            localScale.x *= -1;
        transform.localScale = localScale;
    }
}
