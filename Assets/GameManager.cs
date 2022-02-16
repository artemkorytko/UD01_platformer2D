using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private UIController uiController;
   private SaveSystem saveSystem;
   private GameData gameData;
   private LevelManager levelManager;
   private CameraController cameraController;
   public int Coins => gameData.Coins;
   public int Level => gameData.Level;
   
   public event Action<int> OnCoinsCountChanged = null; 
   private void Awake()
   {
      uiController = FindObjectOfType<UIController>();
      saveSystem = GetComponent<SaveSystem>();
      levelManager = GetComponent<LevelManager>();
      cameraController = FindObjectOfType<CameraController>();
      gameData = saveSystem.LoadData();
      uiController.ShowStartScreen();
      
   }

   private void OnApplicationQuit()
   {
      saveSystem.SaveData(gameData);
   }

   public void StartGame()
   {
      uiController.ShowGameScreen();
      levelManager.InstantiateLevel(Level);
      OnGameStarted();
   }

   
   public void FallGame()
   {
      uiController.ShowFallScreen();
      OnGameEnded();
   }

 
   public void WinGame()
   {
      uiController.ShowWinScreen();
      OnGameEnded();
   }

   private void OnGameStarted()
   {
      cameraController.Initialize(levelManager.Player.transform);
      levelManager.Player.OnWin += WinGame;
      levelManager.Player.OnLost += FallGame;
      levelManager.Player.OnCoinCollected += OnCoinsCollected;
   }
   private void OnGameEnded()
   {
      levelManager.Player.OnWin -= WinGame;
      levelManager.Player.OnLost -= FallGame;
      levelManager.Player.OnCoinCollected -= OnCoinsCollected;
   }
   
   private void OnCoinsCollected()
   {
      gameData.Coins++;
      OnCoinsCountChanged?.Invoke(Coins);
   }
}
