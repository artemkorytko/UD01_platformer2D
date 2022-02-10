using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIController _uiController;
    private SaveSystem _saveSystem;
    private LevelManager _levelManager;
    private CameraController _cameraController;

    private GameData _data;

    public int Coins => _data.Coins;
    public int Level => _data.Level;
    public event Action<int> OnCoinsCountChanged;
    
    private void Awake()
    {
        _uiController = FindObjectOfType<UIController>();
        _saveSystem = GetComponent<SaveSystem>();
        _cameraController = FindObjectOfType<CameraController>();
        _data = _saveSystem.LoadData();
        _uiController.ShowMenuPanel();
        _levelManager = GetComponent<LevelManager>();
    }

    private void OnApplicationQuit()
    {
        _saveSystem.SaveData(_data);
    }

    public void StartGame()
    {
        _uiController.ShowGamePanel();
        _levelManager.InstantiateLevel(Level);
        OnGameStarted();
    }

    public void FailGame()
    {
        _uiController.ShowFailPanel();
        OnGameEnded();
    }
    
    public void WinGame()
    {
        _uiController.ShowWinPanel();
        OnGameEnded();
    }
    
    private void OnGameStarted()
    {
        _levelManager.Character.OnWin += WinGame;
        _levelManager.Character.OnFail += FailGame;
        _levelManager.Character.OnCoinsCollected += OnCoinsCollected;
    }
    
    private void OnGameEnded()
    {
        _levelManager.Character.OnWin -= WinGame;
        _levelManager.Character.OnFail -= FailGame;
        _levelManager.Character.OnCoinsCollected -= OnCoinsCollected;
        _saveSystem.SaveData(_data);
    }

    private void OnCoinsCollected()
    {
        _data.Coins++;
        OnCoinsCountChanged?.Invoke(Coins);
    }
}
