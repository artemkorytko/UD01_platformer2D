using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxValue = 3;
    [SerializeField] private float startValue = 3;
     private UIHealthText uiHealthText;
    
    private float currentValue;
    public event Action OnDie;
    
    public float CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = Mathf.Clamp(value,0,maxValue);
            if (currentValue == 0) OnDie?.Invoke();
            uiHealthText.OnHealthChange(currentValue);
        }
    }

    private void Awake()
    {
        uiHealthText = FindObjectOfType<UIHealthText>();
    }

    private void Start()
    {
        currentValue = startValue;
        uiHealthText.OnHealthChange(currentValue);
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
