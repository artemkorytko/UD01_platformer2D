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

    private Transform target;
    private EnemyAnimationController animationController;

    private void Awake()
    {
        target = CustomRandom.RandomBetweenTwo() ? firstPoint : secondPoint;
        animationController = GetComponentInChildren<EnemyAnimationController>();
    }

    private void Update()
    {
        var direction = (target.position - transform.position).normalized;
        var moveDistance = speed * Time.deltaTime;
        var distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (moveDistance > distanceToTarget)
        {

            target = target == firstPoint ? secondPoint : firstPoint;
            moveDistance = distanceToTarget;

        }
        transform.Translate(direction*moveDistance);
        animationController.SetSpeed((int)Mathf.Sign(direction.x));
        UpdateSide((int)Mathf.Sign(-direction.x));
    }

    private void UpdateSide(float side)
    {
        var localScale = transform.localScale;
        if (Mathf.Sign(localScale.x) != side)
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }
    
}

public static class CustomRandom
{
    public static bool RandomBetweenTwo()
    {
       return Random.Range(0, 2) == 0;
    }
}