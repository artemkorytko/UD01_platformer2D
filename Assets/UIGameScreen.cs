using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameScreen : MonoBehaviour
{
    private UICoinsText coinsText;
    private GameManager gameManager;
   
    private void Awake()
    {
        coinsText = GetComponentInChildren<UICoinsText>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        coinsText.OnCoinsChange(gameManager.Coins);
        gameManager.OnCoinsCountChanged += coinsText.OnCoinsChange;
    }

    private void OnDestroy()
    {
        gameManager.OnCoinsCountChanged -= coinsText.OnCoinsChange;
    }
}
