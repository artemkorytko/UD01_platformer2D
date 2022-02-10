using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousObject : MonoBehaviour
{
    [SerializeField] protected float _damageValue = 1f;
    [SerializeField] protected float _damageImpulse = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            SetDamage(health);
        }
    }

    protected virtual void SetDamage(Health health)
    {
        health.HealthValue -= _damageValue;

        Rigidbody2D rigidbody2D = health.gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(Vector3.up * _damageImpulse, ForceMode2D.Impulse);
    }
}
