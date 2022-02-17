using System;
using UnityEngine;

public class TouchChecker : MonoBehaviour
{
    public event Action<bool, GameObject> OnTouch;

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnTouch?.Invoke(true, other.gameObject);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        OnTouch?.Invoke(false, other.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTouch?.Invoke(true, other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTouch?.Invoke(false, other.gameObject);
    }
}
