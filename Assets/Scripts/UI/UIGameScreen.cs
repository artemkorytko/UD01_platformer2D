using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameScreen : MonoBehaviour
{
    [SerializeField] private UICoinText _coinsText = null;
    [SerializeField] private GameManager _gameManager = null;

    private void OnEnable()
    {
        _coinsText.UpdateText(_gameManager.Coins);
        _gameManager.OnCoinCountChanged += _coinsText.UpdateText;
    }

    private void OnDisable()
    {
        _gameManager.OnCoinCountChanged -= _coinsText.UpdateText;
    }
}
