using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _startSrceen = null;
    [SerializeField] private GameObject _gameScreen = null;
    [SerializeField] private GameObject _winScreen = null;
    [SerializeField] private GameObject _failScreen = null;

    private GameObject _currentScreen = null;

    public void ShowStartScreen()
    {
        DisableCurrentScreen();
        _currentScreen = _startSrceen;
        _currentScreen.SetActive(true);
    }

    public void ShowGameScreen()
    {
        DisableCurrentScreen();
        _currentScreen = _gameScreen;
        _currentScreen.SetActive(true);
    }

    public void ShowWinScreen()
    {
        DisableCurrentScreen();
        _currentScreen = _winScreen;
        _currentScreen.SetActive(true);
    }

    public void ShowFailScreen()
    {
        DisableCurrentScreen();
        _currentScreen = _failScreen;
        _currentScreen.SetActive(true);
    }

    private void DisableCurrentScreen()
    {
        if (_currentScreen == null) return;

        _currentScreen.SetActive(false);
    }
}
