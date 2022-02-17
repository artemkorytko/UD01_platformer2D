using System;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float value = 1f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var health = other.gameObject.GetComponent<Health>();

        if (health != null)
        {
            SetDamage(health);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.gameObject.GetComponent<Health>();

        if (health != null)
        {
            SetDamage(health);
        }
    }


    private void SetDamage(Health health)
    {
        health.CurrentValue -= value;
    }
}
