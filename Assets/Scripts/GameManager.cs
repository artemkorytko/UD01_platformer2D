using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
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
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
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
        if(_levelManager.CurrentLevel == null)
            _levelManager.InstantiateLevel(Level);
        _levelManager.CurrentLevel.SetActive(true);    
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

    public void ExitGame()
    {
        _uiController.ShowMenuPanel();
        OnGameEnded();
        _levelManager.DisableCurrentLevel();
    }
    
    private void OnGameStarted()
    {
        _levelManager.Character.OnWin += WinGame;
        _levelManager.Character.OnFail += FailGame;
        _levelManager.Character.OnCoinsCollected += OnCoinsCollected;
        _cameraController.Initialize(_levelManager.Character.transform);
    }
    
    private void OnGameEnded()
    {
        _levelManager.Character.OnWin -= WinGame;
        _levelManager.Character.OnFail -= FailGame;
        _levelManager.Character.OnCoinsCollected -= OnCoinsCollected;
        _saveSystem.SaveData(_data);
        _cameraController.Deactivate();
    }

    private void OnCoinsCollected()
    {
        _data.Coins++;
        OnCoinsCountChanged?.Invoke(Coins);
    }
}
