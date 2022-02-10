using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIController _uiController = null;
    [SerializeField] private LevelManager _levelManager = null;
    [SerializeField] private SaveController _saveController = null;
    [SerializeField] private CameraMovement _cameraMovement = null;

    private GameData _gameData = null;

    public int LevelIdex => _gameData.Level;
    public int Coins => _gameData.Coins;
    public System.Action<int> OnCoinCountChanged = null;

    private void Awake()
    {
        _gameData = _saveController.LoadData();
        _uiController.ShowStartScreen();
    }

    public void StartGame()
    {
        _levelManager.InstantiateLevel(_gameData.Level);
        _uiController.ShowGameScreen();
        OnGameStarted();
    }

    public void WinGame()
    {
        _gameData.Level++;
        _uiController.ShowWinScreen();
        OnGameEnded();
    }

    public void FailGame()
    {
        _uiController.ShowFailScreen();
        OnGameEnded();
    }

    private  void OnGameStarted()
    {
        _cameraMovement.Initialize(_levelManager.Player.transform);
        _levelManager.Player.OnWin += WinGame;
        _levelManager.Player.OnDead += FailGame;
        _levelManager.Player.OnCoinCollected += OnCoinCollected;
    }

    private void OnGameEnded()
    {
        _levelManager.Player.OnWin -= WinGame;
        _levelManager.Player.OnDead -= FailGame;
        _levelManager.Player.OnCoinCollected -= OnCoinCollected;
        _saveController.SaveData(_gameData);
    }

    private void OnCoinCollected()
    {
        _gameData.Coins++;
        OnCoinCountChanged?.Invoke(_gameData.Coins);
    }

    private void OnApplicationQuit()
    {
        _saveController.SaveData(_gameData);
    }
}
