using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue = 3f;
    [SerializeField] private float _startValue = 3f;

    private float _currentValue = 0f;
    public float HealthValue
    {
        get
        {
            return _currentValue;
        }

        set
        {
            _currentValue = Mathf.Clamp(value, 0f, _maxValue);
            if (_currentValue == 0f) OnDie?.Invoke();
        }
    }

    public System.Action OnDie = null;

    private void Start()
    {
        _currentValue = _startValue;
    }

    [ContextMenu("Set dead")]
    public void SetDead()
    {
        HealthValue = 0f;
    }
}
