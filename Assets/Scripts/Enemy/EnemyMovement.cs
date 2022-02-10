using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Transform _firstPoint = null;
    [SerializeField] private Transform _secondPoint = null;
    [SerializeField] private Transform _startTarget = null;

    private Transform _target = null;
    private EnemyAnimationController _animationController = null;

    private void Start()
    {
        _target = _startTarget;
        _animationController = GetComponent<EnemyAnimationController>();
    }

    private void Update()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        float moveDistance = _speed * Time.deltaTime;
        float distanceToTarget = Vector3.Distance(_target.position, transform.position);

        if (moveDistance > distanceToTarget)
        {
            moveDistance = distanceToTarget;

            if (_target == _firstPoint)
            {
                _target = _secondPoint;
            }
            else
            {
                _target = _firstPoint;
            }
        }

        transform.Translate(direction * moveDistance);
        _animationController.SetSpeedDirection((int)Mathf.Sign(direction.x));
        UpdateSide((int)Mathf.Sign(direction.x));
    }

    private void UpdateSide(int side)
    {
        Vector3 localScale = transform.localScale;

        if (Mathf.Sign(localScale.x) != side)
        {
            localScale.x *= -1f;
        }

        transform.localScale = localScale;
    }
}
