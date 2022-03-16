using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxValue = 3f;
    [SerializeField] private float startValue = 3f;

    private float _currentValue;

    public event Action OnDie;

    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            _currentValue = Mathf.Clamp(value, 0, maxValue);
            if (_currentValue == 0)
            {
                OnDie?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        _currentValue = startValue;
    }

    [ContextMenu("Set Dead")]
    public void SetDead()
    {
        CurrentValue = 0;
    }
    
    [ContextMenu("Set Max Value")]
    public void SetMaxValue()
    {
        CurrentValue = maxValue;
    }
}
