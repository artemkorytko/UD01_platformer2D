using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class UICoinsText : MonoBehaviour
{
    private TextMeshProUGUI text;
   
    
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnCoinsChange(int value)
    {
        text.text = value.ToString();
    }
}