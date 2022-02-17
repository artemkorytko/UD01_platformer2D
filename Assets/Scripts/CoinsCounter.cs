using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        GameManager.Instance.OnCoinsCountChanged += UpdateText;
        UpdateText(GameManager.Instance.Coins);
    }

    private void UpdateText(int coins)
    {
        _text.text = coins.ToString();
    }
}
